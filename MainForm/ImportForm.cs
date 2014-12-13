using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SharePoint;
using System.IO;
using CsvHelper;
using Trilogen.Helpers;
using System.Configuration;
using System.Deployment;
using System.Reflection;

namespace Trilogen
{
    public partial class ImportForm : Form
    {
        SharepointManager spManager;
        DataTable dtMapping = new DataTable();
        string[] csvHeaders = null;
        DataGridViewComboBoxCell cbSpListItems = null;
        CsvParser csvParser = null;
        List<string[]> csvRecords = null;

        public ImportForm()
        {
            // Handle the ApplicationExit event to know when the application is exiting.
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            // init
            InitializeComponent();

            // load settings
            txtUsername.Text = Properties.Settings.Default.Username;
            txtDomain.Text = Properties.Settings.Default.Domain;
            txtSiteUrl.Text = Properties.Settings.Default.SiteURL;

            // lock other groups
            gbValidate.Enabled = false;
            gbImport.Enabled = false;
            btnValidateFile.Enabled = false;

            // setup mappings
            dtMapping = new DataTable();
            cbSpListItems = new DataGridViewComboBoxCell();
        }

        public bool Connect(string siteUrl, string username, string password, string domain)
        {
            bool connected = false;

            try
            {
                // sp manager
                spManager = new SharepointManager(siteUrl, username, password, domain);

                // sp lists
                var spLists = spManager.GetLists();

                // lists exists
                if (spLists != null)
                {
                    // get num items
                    int numListItems = spLists.Count;

                    // has items
                    if (numListItems > 0)
                    {
                        for (int i = 0; i < numListItems; i++)
                        {
                            // list item ref
                            var listItem = spLists[i];

                            // new item
                            ComboBoxItem item = new ComboBoxItem
                            {
                                Text = listItem.Title,
                                Value = listItem.Id,
                                Fields = listItem.Fields
                            };

                            // add to list
                            cbListname.Items.Add(item);
                        }
                    }

                    // success
                    connected = true;

                    // save config values
                    Properties.Settings.Default.Username = txtUsername.Text;
                    Properties.Settings.Default.Domain = txtDomain.Text;
                    Properties.Settings.Default.SiteURL = txtSiteUrl.Text;

                    // save settings
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error connecting to SharePoint site", MessageBoxButtons.OK, MessageBoxIcon.Error);

                connected = false;
            }

            return connected;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult openFileResult = openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            txtImportFilename.Text = openFileDialog.FileName;

            // import disabled until file is validated
            gbImport.Enabled = false;
            btnValidateFile.Enabled = true;

            // reset label
            lblNumRecords.Text = "0 records";
        }

        private bool ValidateCSVFile(string filename)
        {
            // file is not empty
            if (!string.IsNullOrEmpty(filename))
            {
                // file exists
                if (File.Exists(filename))
                {
                    // file stream
                    StreamReader csvStreamReader = File.OpenText(filename);

                    // csv parser
                    csvParser = new CsvParser(csvStreamReader);

                    // read headers
                    csvHeaders = csvParser.Read();

                    if (csvHeaders == null)
                        return false;

                    // clear existing records
                    dgvMappings.Rows.Clear();

                    // loop through headers
                    foreach (var header in csvHeaders)
                    {
                        if (!string.IsNullOrEmpty(header))
                        {
                            // add datagrid row
                            DataGridViewRow newRow = new DataGridViewRow();

                            // checkbox
                            DataGridViewCheckBoxCell cbCell = new DataGridViewCheckBoxCell();

                            // text box
                            DataGridViewTextBoxCell txtHeader = new DataGridViewTextBoxCell();
                            txtHeader.Value = header;

                            DataGridViewComboBoxCell cbMappings = new DataGridViewComboBoxCell();

                            // data type box
                            DataGridViewComboBoxCell cbType = new DataGridViewComboBoxCell();

                            // add items
                            cbType.Items.Add("Number");
                            cbType.Items.Add("Text");
                            cbType.Items.Add("Html");

                            // add cells
                            newRow.Cells.Add(cbCell);
                            newRow.Cells.Add(txtHeader);
                            newRow.Cells.Add(cbMappings);
                            newRow.Cells.Add(cbType);

                            // add row
                            dgvMappings.Rows.Add(newRow); 

                            // set read only after row is added
                            cbMappings.ReadOnly = true;
                        }
                    }

                    // new records obj
                    csvRecords = new List<string[]>();

                    // continue reading rest of records
                    bool hasRecords = true;

                    // loop records
                    while (hasRecords)
                    {
                        // record
                        string[] record = csvParser.Read();

                        // record does not exist
                        if (record == null)
                        {
                            hasRecords = false;

                            break;
                        }

                        // record exists
                        if (record != null)
                            csvRecords.Add(record);
                    }

                    return true;
                }
            }

            return false;
        }

        private void btnValidateFile_Click(object sender, EventArgs e)
        {
            // filename
            string csvFile = txtImportFilename.Text;

            // file path set
            if (!string.IsNullOrEmpty(csvFile) && csvFile.Length > 0)
            {
                // file exists
                if (File.Exists(csvFile))
                {
                    // get csv headers
                    bool testCSV = ValidateCSVFile(csvFile);

                    // valid file
                    if (!testCSV)
                    {
                        MessageBox.Show("Unable to validate CSV file", "Error parsing CSV file", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    // has records
                    if (csvRecords != null && csvRecords.Count > 0)
                    {
                        // enable sharepoint import
                        gbImport.Enabled = true;
                        lblNumRecords.Text = csvRecords.Count + " records";
                    }
                    else
                    {
                        MessageBox.Show("CSV file has no records to import!", "No records to import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string siteUrl = txtSiteUrl.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string domain = txtDomain.Text;

            bool result = Connect(siteUrl, username, password, domain);

            if (result)
            {
                // button flags
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;

                // enable csv processing
                gbValidate.Enabled = true;
            }
        }
        private void UpdateMappings()
        {
            // we have a selection
            if (cbListname.SelectedIndex > -1)
            {
                // populate columns
                ComboBoxItem selectedItem = (ComboBoxItem)cbListname.Items[cbListname.SelectedIndex];

                // sp fields
                var fields = selectedItem.Fields;

                // update datagrid
                for (int i = 0; i < dgvMappings.Rows.Count; i++)
                {
                    // column
                    string columnName = Convert.ToString(dgvMappings.Rows[i].Cells[1].Value);

                    // get combobox from each row
                    DataGridViewComboBoxCell cbCurrentMapping = (DataGridViewComboBoxCell)dgvMappings.Rows[i].Cells[2];

                    // attach event

                    // get data type combo box
                    DataGridViewComboBoxCell cbType = (DataGridViewComboBoxCell)dgvMappings.Rows[i].Cells[3];

                    // clear current mapping items
                    cbCurrentMapping.Value = null;
                    cbType.Value = null;

                    // enable combo box for mappings
                    cbCurrentMapping.ReadOnly = false;
                    cbType.ReadOnly = true;

                    // clear items in box
                    cbCurrentMapping.Items.Clear();

                    int fieldIndex = 0;
                    string matchedProperty = string.Empty;

                    // add list fields to drop list items
                    foreach (var field in fields)
                    {
                        // not read only
                        if (!field.ReadOnlyField)
                        {
                            cbCurrentMapping.Items.Add(field.EntityPropertyName);

                            if (columnName == field.EntityPropertyName)
                            {
                                matchedProperty = field.EntityPropertyName;
                            }
                        }

                        fieldIndex++;
                    }

                    // found a field match
                    if (!string.IsNullOrEmpty(matchedProperty))
                    {
                        cbCurrentMapping.Value = matchedProperty;
                    }  
                }  
            }
        }

        private void cbListname_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update mappings
            UpdateMappings();
        }

        bool HasSelectedMappingColumn()
        {
            for (int i = 0; i < dgvMappings.Rows.Count; i++)
            {
                DataGridViewRow row = dgvMappings.Rows[i];
                DataGridViewCheckBoxCell cbCell = (DataGridViewCheckBoxCell)row.Cells[0];
                bool isChecked = (Convert.ToBoolean(cbCell.Value));

                if (isChecked)
                    return true;
            }

            return false;
        }

        bool HasSelectedDataType()
        {
            // loop through each row
            for (int i = 0; i < dgvMappings.Rows.Count; i++)
            {
                // row
                DataGridViewRow row = dgvMappings.Rows[i];

                // combo box type
                DataGridViewComboBoxCell cbType = (DataGridViewComboBoxCell)row.Cells[3];

                // checkbox cell
                DataGridViewCheckBoxCell cbCell = (DataGridViewCheckBoxCell)row.Cells[0];
                
                // is checked
                bool isChecked = (Convert.ToBoolean(cbCell.Value));

                // column selected
                if (isChecked)
                {
                    // get string ref
                    string strDataType = Convert.ToString(cbType.Value);

                    // has been set
                    if (!string.IsNullOrEmpty(strDataType))
                        return true;
                }
            }

            return false;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // validate column selection
            if (!HasSelectedMappingColumn())
            {
                MessageBox.Show("You must select at least 1 column to import", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // we are connected to sp site
            if (spManager != null)
            {
                // vars
                string listName = string.Empty;
                Guid listId = Guid.Empty;

                // has list been selected
                if (cbListname.SelectedIndex > -1)
                {
                    ComboBoxItem selectedItem = (ComboBoxItem)cbListname.Items[cbListname.SelectedIndex];

                    // select item exists
                    if (selectedItem != null)
                    {
                        listName = selectedItem.Text;
                        listId = (Guid)selectedItem.Value;
                    }
                }
                else
                {
                    // list name is new user input
                    listName = cbListname.Text;
                }

                // selected list
                if (listId != Guid.Empty)
                {
                    bool appendResult = spManager.Import(listId, csvHeaders, csvRecords, dgvMappings);

                    if (!appendResult)
                    {
                        MessageBox.Show("Error importing CSV file into SharePoint", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        int numRecordsImported = csvRecords.Count;

                        MessageBox.Show("CSV file imported " + numRecordsImported + " records into '" + listName + "' successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // reset selected items
                        cbListname.Text = string.Empty;
                        cbListname.SelectedIndex = -1;

                        // reset mappings
                        UpdateMappings();
                    }
                }
                else
                {
                    // validation
                    // make sure data type is selected
                    if (listNameExists(listName))
                    {
                        // show error msg
                        MessageBox.Show("List name already exists! Please enter a new name or select one from the list and try again.", 
                            "List name taken", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        return;
                    }

                    // make sure data type is selected for checked columns
                    if (!HasSelectedDataType())
                    {
                        // show error msg
                        MessageBox.Show("You must select a data type for each new column that will be imported into SharePoint.", 
                            "Missing data type selection", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    // create new list
                    bool importResult = false;

                    // try sharepoint import
                    try
                    {
                        importResult = spManager.Import(listName, csvHeaders, csvRecords, dgvMappings);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("SharePoint error: " + ex.Message);

                        importResult = false;
                    } 

                    
                    // import failed
                    if(!importResult)
                    {
                        MessageBox.Show("CSV file has been imported successfully!\n\nA new list has been created: '" + listName + "'", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            // item is checked
            if (cbSelectAll.Checked)
            {
                // loop through columns
                for (int i = 0; i < dgvMappings.Rows.Count; i++)
                {
                    // select item
                    DataGridViewCheckBoxCell cbCell = (DataGridViewCheckBoxCell)dgvMappings.Rows[i].Cells[0];

                    cbCell.Value = true;
                }
            }
            else
            {
                // loop through columns
                for (int i = 0; i < dgvMappings.Rows.Count; i++)
                {
                    // select item
                    DataGridViewCheckBoxCell cbCell = (DataGridViewCheckBoxCell)dgvMappings.Rows[i].Cells[0];

                    cbCell.Value = false;
                }
            }
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {

        }

        private void cbListname_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateMappings();
        }

        private bool listNameExists(string listName)
        {
            foreach (var comboItem in cbListname.Items)
            {
                ComboBoxItem item = (ComboBoxItem)comboItem;

                if (item.Text == listName)
                {
                    return true;
                }
            }

            return false;
        }

        private void cbListname_TextUpdate(object sender, EventArgs e)
        {
            // check if list exists in box, else new list
            bool listExists = listNameExists(cbListname.Text);

            // list not found
            if (!listExists)
            {
                // clear mappings
                foreach (DataGridViewRow row in dgvMappings.Rows)
                {
                    // data type required
                    DataGridViewComboBoxCell cbType = (DataGridViewComboBoxCell)row.Cells[3];
                    cbType.ReadOnly = false;

                    // mapping cell
                    DataGridViewComboBoxCell cbCurrentMapping = (DataGridViewComboBoxCell)row.Cells[2];

                    // unselect any selected mappings
                    cbCurrentMapping.Value = null;
                    cbCurrentMapping.ReadOnly = true;
                    
                    cbCurrentMapping.Items.Clear();
                }
            }
        }

        private string CleanInput(string input)
        {
            // Replace invalid characters with empty strings. 
            try
            {
                return Regex.Replace(input, @"[^\w\.@ -]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,  
            // we should return Empty. 
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        private void dgvMappings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMappings != null && dgvMappings.Rows.Count > 0)
            {
                // mapping select box changed value
                if (e.ColumnIndex == 2)
                {
                    // if row not selected, check box
                    DataGridViewCheckBoxCell cbCell = (DataGridViewCheckBoxCell)dgvMappings.Rows[e.RowIndex].Cells[0];

                    // check box
                    cbCell.Value = true;
                }
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            // flags
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            
            // reset validate group box
            gbValidate.Enabled = false;
            txtImportFilename.Text = string.Empty;
            lblNumRecords.Text = "0 records";

            // reset import group box
            dgvMappings.Rows.Clear(); // clear rows in datagridview
            cbListname.Items.Clear(); // clear sharepoint list drop down
            cbListname.Text = string.Empty;
            gbImport.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // reset mapping grid
            foreach (DataGridViewRow row in dgvMappings.Rows)
            {
                // checkbox
                DataGridViewCheckBoxCell cbSelected = (DataGridViewCheckBoxCell)row.Cells[0];

                if (cbSelected != null)
                {
                    bool isSelected = Convert.ToBoolean(cbSelected.Value);
                    
                    if (isSelected)
                        cbSelected.Value = false;
                }
                
                // mapping drop down
                DataGridViewComboBoxCell cbMapping = (DataGridViewComboBoxCell)row.Cells[2];
                
                if(cbMapping != null)
                    cbMapping.Value = null;

                // data type
                DataGridViewComboBoxCell cbType = (DataGridViewComboBoxCell)row.Cells[3];
                
                if (cbType != null)
                    cbType.Value = null;
            }

            // refresh mappings
            dgvMappings.Update();
        }
    }
}
