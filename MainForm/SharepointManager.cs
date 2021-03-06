﻿using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Trilogen
{
    public delegate void ImportHandler(ImportEvent e);
    
    public class ImportEvent : EventArgs
    {
        public bool Success = false;
        public string ErrorMessage = string.Empty;
        public int RecordsImported = 0;
        public int TotalRecords = 0;
        public float ImportProgressValue = 0f;
    }

    public class SharepointManager
    {
        // members
        private ClientContext _context;
        private Web _web;
        private int maxListItemsPerQuery = 40;
       
        // events
        public event ImportHandler ImportCompleted;
        public event ImportHandler ImportFailed;
        public event ImportHandler ImportUpdate;

        public SharepointManager(string siteUrl, string username, string password, string domain)
        {
            // credentials
            NetworkCredential myCred = new NetworkCredential(username, password, domain);

            // new context
            _context = new ClientContext(siteUrl);
            _context.Credentials = myCred;

            // web ref
            _web = _context.Web;

            // load context
            _context.Load(_web);
            _context.ExecuteQuery();
        }

        private void importCompleted(ImportEvent e)
        {
            ImportCompleted(e);
        }

        private void importFailed(ImportEvent e)
        {
            ImportFailed(e);
        }

        private void importUpdate(ImportEvent e)
        {
            ImportUpdate(e);
        }

        public List<SharepointList> GetLists()
        {
            List<SharepointList> spLists = new List<SharepointList>();

            _context.Load(_web.Lists,
             lists => lists.Include(list => list.Title, // For each list, retrieve Title and Id. 
                                    list => list.Id,
                                    list => list.Fields));

            // Execute query. 
            _context.ExecuteQuery();

            // Enumerate the web.Lists. 
            foreach (List list in _web.Lists)
            {
                spLists.Add(new SharepointList
                {
                    Id = list.Id,
                    Title = list.Title,
                    Fields = list.Fields
                });
            }

            return spLists;
        }

        private string GetSharepointMappingFromCSVColumn(string header, DataGridView dgvMapping)
        {
            for (int i = 0; i < dgvMapping.Rows.Count; i++)
            {
                DataGridViewRow row = dgvMapping.Rows[i];
                string headerCell = Convert.ToString(row.Cells[1].Value);

                if (string.IsNullOrEmpty(headerCell))
                    continue;

                if (headerCell == header)
                {
                    DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[2];

                    return Convert.ToString(cbCell.ValueMember);
                } 
            }

            return string.Empty;
        }

        // import to existing list
        public bool Import(Guid listId, string[] headers, List<string[]> records, DataGridView mappings)
        {
            // validation
            if (listId == Guid.Empty)
                return false;

            if (records == null)
                return false;

            if (mappings == null)
                return false;

            try
            {
                // get sp list
                List selectedList = _web.Lists.GetById(listId);

                // try load list
                try
                {
                    // get list including fields
                    _context.Load(selectedList, l => l.Fields);
                    _context.ExecuteQuery();

                }
                catch (Exception e)
                {
                    // send failed event
                    importFailed(new ImportEvent
                    {
                        ErrorMessage = e.Message,
                        Success = false
                    });

                    // list not found
                    return false;
                }

                // get list of valid headers
                List<string> validHeaders = new List<string>();

                // load headers
                foreach (var column in selectedList.Fields)
                {
                    // not read only
                    if (!column.ReadOnlyField)
                    {
                        // add valid header or selected list
                        validHeaders.Add(column.InternalName);
                    }
                }

                // record index
                int recordIndex = 0;

                // total items processed
                float totalRecordsProcessed = 0;

                // progress value
                float progressValue = 0;
                float totalRecords = (float)records.Count;

                // loop through records
                foreach (string[] record in records)
                {
                    // commit changes every 20 records
                    if (recordIndex > maxListItemsPerQuery)
                    {
                        // commit changes
                        _context.ExecuteQuery();

                        // reset index
                        recordIndex = 0;

                        // update progress bar
                        progressValue = (totalRecordsProcessed / totalRecords) * 100;

                        // send update event
                        importUpdate(new ImportEvent
                        {
                            ImportProgressValue = progressValue,
                            RecordsImported = (int)totalRecordsProcessed,
                            TotalRecords = (int)totalRecords,
                            Success = true,
                            ErrorMessage = string.Empty
                        });
                    }

                    // new list item
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem newItem = selectedList.AddItem(itemCreateInfo);

                    // loop through 
                    for (int i = 0; i < headers.Length; i++)
                    {
                        string currentHeader = headers[i];

                        // header is not valid, move on
                        if (validHeaders.Contains(currentHeader))
                        {
                            // populate new item
                            newItem[currentHeader] = record[i];

                            // update item
                            newItem.Update();
                        }
                    }

                    // update total count of processed items
                    totalRecordsProcessed++;

                    // incrememnt
                    recordIndex++;
                }

                // commit remaining changes
                _context.ExecuteQuery();

                // send update event
                importUpdate(new ImportEvent
                {
                    ImportProgressValue = progressValue,
                    RecordsImported = (int)totalRecordsProcessed,
                    TotalRecords = (int)totalRecords,
                    Success = true,
                    ErrorMessage = string.Empty
                });

                // send complete event
                importCompleted(new ImportEvent
                {
                    Success = true,
                    RecordsImported = (int)totalRecordsProcessed,
                    TotalRecords = records.Count,
                    ErrorMessage = string.Empty,
                    ImportProgressValue = 100f
                });
            }
            catch (Exception ex)
            {
                // send failed event
                importFailed(new ImportEvent
                {
                    ErrorMessage = ex.Message,
                    Success = false
                });

                return false;
            }

            // success
            return true;
        }

        // create new list
        public bool Import(string listName, string[] headers, List<string[]> records, DataGridView mappings)
        {
            // validation
            if (string.IsNullOrEmpty(listName))
                return false;

            if (records == null)
                return false;

            if (mappings == null)
                return false;

            
            // date
            DateTime date = DateTime.Now;

            try
            {
                // create list
                ListCreationInformation creationInfo = new ListCreationInformation();
                creationInfo.Title = listName;
                creationInfo.TemplateType = (int)ListTemplateType.GenericList;

                // list details
                List list = _web.Lists.Add(creationInfo);
                list.Description = "Imported on " + date.ToString("G");
                list.Update();

                // commit changes
                _context.ExecuteQuery();

                // create list columns based on selected columns
                for (int i = 0; i < mappings.Rows.Count; i++)
                {
                    // column header
                    string columnHeader = Convert.ToString(mappings.Rows[i].Cells[1].Value);

                    // data type
                    string dataType = Convert.ToString(mappings.Rows[i].Cells[3].Value);

                    // is checked
                    bool isChecked = Convert.ToBoolean(mappings.Rows[i].Cells[0].Value);

                    // only add columns for items that were selected
                    if (isChecked)
                    {
                        // new field
                        Field newField = null;

                        // check column types
                        if (dataType == "Number")
                        {
                            // column type is number
                            newField = list.Fields.AddFieldAsXml("<Field DisplayName='" + columnHeader + "' Name='" + columnHeader + "' Type='Number' />", true, AddFieldOptions.DefaultValue);
                        }
                        else if (dataType == "Html")
                        {
                            // column type is html
                            newField = list.Fields.AddFieldAsXml("<Field DisplayName='" + columnHeader + "' Name='" + columnHeader + "' RestrictedMode='TRUE' Type='Note' NumLines='6' RichText='TRUE' RichTextMode='FullHtml' Required='FALSE' EnforceUniqueValues='FALSE' Indexed='FALSE' IsolateStyles='TRUE' AppendOnly='FALSE' />", true, AddFieldOptions.DefaultValue);
                        }
                        else
                        {
                            // all others are text
                            newField = list.Fields.AddFieldAsXml("<Field DisplayName='" + columnHeader + "' Name='" + columnHeader + "' Type='Text' />", true, AddFieldOptions.DefaultValue);
                        }

                        // new field exists
                        if (newField != null)
                        {
                            newField.Update();
                        }
                    }
                }

                // commit changes
                _context.ExecuteQuery();

                int recordIndex = 0;

                // total items processed
                float totalRecordsProcessed = 0;
                float totalRecords = (int)records.Count;

                // progress value
                float progressValue = 0;

                // loop through records
                foreach (string[] record in records)
                {
                    // commit changes every 20 records
                    if (recordIndex > maxListItemsPerQuery)
                    {
                        // commit changes
                        _context.ExecuteQuery();

                        // reset index
                        recordIndex = 0;

                        // update progress bar
                        progressValue = (totalRecordsProcessed / totalRecords) * 100f;

                        // send update event
                        importUpdate(new ImportEvent
                        {
                            ImportProgressValue = progressValue,
                            RecordsImported = (int)totalRecordsProcessed,
                            TotalRecords = (int)totalRecords,
                            Success = true,
                            ErrorMessage = string.Empty
                        });
                    }

                    // new list item
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem newItem = list.AddItem(itemCreateInfo);

                    // loop through 
                    for (int i = 0; i < headers.Length; i++)
                    {
                        // get record details
                        string recordHeader = headers[i];

                        // next record if column header is blank
                        if (string.IsNullOrEmpty(recordHeader))
                            continue;

                        // column/header is selected to be imported
                        if (isItemChecked(recordHeader, mappings))
                        {
                            string recordContent = record[i];

                            // populate new item
                            newItem[recordHeader] = recordContent;

                            // update
                            newItem.Update();
                        }
                    }

                    // update total items processed
                    totalRecordsProcessed++;

                    // incrememnt
                    recordIndex++;
                }

                // commit remaining changes
                _context.ExecuteQuery();

                // send update event
                importUpdate(new ImportEvent
                {
                    ImportProgressValue = progressValue,
                    RecordsImported = (int)totalRecordsProcessed,
                    TotalRecords = (int)totalRecords,
                    Success = true,
                    ErrorMessage = string.Empty
                });

                // send complete event
                importCompleted(new ImportEvent
                {
                    Success = true,
                    RecordsImported = (int)totalRecordsProcessed,
                    TotalRecords = records.Count,
                    ErrorMessage = string.Empty,
                    ImportProgressValue = 100f
                });
            }
            catch (Exception ex)
            {
                // send failed event
                importFailed(new ImportEvent
                {
                    ErrorMessage = ex.Message,
                    Success = false
                });

                return false;
            }

            // success
            return true;
        }

        private bool isItemChecked(string columnHeader, DataGridView dgvMapping)
        {
            if (dgvMapping == null)
                return false;

            for (int i = 0; i < dgvMapping.Rows.Count; i++)
            {
                DataGridViewRow row = dgvMapping.Rows[i];
                DataGridViewCheckBoxCell cbCell = (DataGridViewCheckBoxCell)row.Cells[0];

                bool isChecked = Convert.ToBoolean(cbCell.Value);

                if (isChecked)
                    return true;

            }

            return false;
        }

        public class SharepointList
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public FieldCollection Fields { get; set; }
        }
    }
}
