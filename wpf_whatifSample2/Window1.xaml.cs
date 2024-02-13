//using Azure.Storage.Blobs;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using static What_if_api_WPF_App.MainWindow;

namespace wpf_whatifSample2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Class1 class_name1;

        private MainWindow MainWindow;

        public Window1(Class1 class_name2, MainWindow window)
        {
            InitializeComponent();

            this.class_name1 = class_name2;

            this.MainWindow = window;
        }
        public string str2, str3;
        private void Template_browse(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = openFileDlg.ShowDialog();

            //string connectionString = "DefaultEndpointsProtocol=https;AccountName=armstoraeg123;AccountKey=2lmkGWhqQUoh4GAIaqc83i8rku7Q4EogXWQ/Ttv7nPtKWXgvBXBdPrT1WWtSu8ZFMjI98mPwX25B+AStBSmBiQ==;EndpointSuffix=core.windows.net";

            //string containerName = "armfiles";

            if (result == true)
            {
                Template_text.Text = openFileDlg.FileName;

                class_name1.txt1 = Template_text.Text.ToLower();

                string filePath = class_name1.txt1;

                if (System.IO.Path.GetExtension(class_name1.txt1).EndsWith(".json"))
                {
                    //if (File.Exists(filePath))
                    //{
                    class_name1.str2 = File.ReadAllText(openFileDlg.FileName);

                    //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                    //BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                    //class_name1.blobName = System.IO.Path.GetFileName(filePath);

                    //BlobClient blobClient = containerClient.GetBlobClient(class_name1.blobName);

                    //await blobClient.UploadAsync(filePath, true);

                }
                else
                {
                    MessageBox.Show("Upload the template file in json format only");

                    Template_text.Clear();
                }
            }
        }
        public void Param_browse(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = openFileDlg.ShowDialog();

            if (result == true)
            {
                Parameter_text.Text = openFileDlg.FileName;

                class_name1.txt2 = Template_text.Text.ToLower();
                
                string txt = Parameter_text.Text.ToLower();

                if (System.IO.Path.GetExtension(txt).EndsWith(".json"))
                {
                    class_name1.str3 = System.IO.File.ReadAllText(openFileDlg.FileName);
                }
                else
                {
                    MessageBox.Show("Upload the parameter file in json format only");
                    Parameter_text.Clear();
                }

                //  str3 = System.IO.File.ReadAllText(openFileDlg.FileName);

            }
            Validate_btn.IsEnabled = true;
        }
        private async void Deploy_button(object sender, RoutedEventArgs e)
        {
            string caption = "Dialogue Window";

            MessageBoxResult result = MessageBox.Show("Are you sure to deploy??", caption, MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:

                    Validate_btn.IsEnabled = false;

                    Deploy_btn.IsEnabled = false;

                    Back_btn.IsEnabled = false;

                    Resource_text.IsEnabled = false;

                    Tmp_browse_btn.IsEnabled = false;

                    Para_browse_btn.IsEnabled = false;

                    Template_text.IsEnabled = false;

                    Parameter_text.IsEnabled = false;

                    Dep_name_text.IsEnabled = false;

                    drop.IsEnabled = false;

                    dataGrid1.Visibility = Visibility.Collapsed;

                    loader.Visibility = Visibility.Visible;

                    ArmDeploymentContent content2;

                    ClientSecretCredential clientSecretCredential = new ClientSecretCredential(

                     clientId: class_name1.cli_id,

                    tenantId: class_name1.Ten_id,

                    clientSecret: class_name1.cli_sect);

                    // AccessToken accessToken = clientSecretCredential.GetTokenAsync(new TokenRequestContext(new[] { "https://management.azure.com/.default" })).Result;
                    //Console.WriteLine(accessToken);

                    TokenCredential credential = clientSecretCredential;

                    ArmClient client = new ArmClient(credential);

                    // this example assumes you already have this ArmDeploymentResource created on azure
                    // for more information of creating ArmDeploymentResource, please refer to the document of ArmDeploymentResource
                    string subscriptionId = class_name1.sub_id;

                    string resourceGroupName = class_name1.rsc_grp;

                    string scope = $"/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}";

                    string deploymentName = class_name1.dep_name;

                    ResourceIdentifier armDeploymentResourceId = ArmDeploymentResource.CreateResourceIdentifier(scope, deploymentName);

                    ArmDeploymentResource armDeployment = client.GetArmDeploymentResource(armDeploymentResourceId);

                    string deploymentModeString = drop.SelectionBoxItem.ToString();

                    ArmDeploymentMode deploymentMode = (ArmDeploymentMode)Enum.Parse(typeof(ArmDeploymentMode), deploymentModeString);

                   content2 = new ArmDeploymentContent(new ArmDeploymentProperties(deploymentMode)
                    {
                        // use the FromStream method to create a BinaryData object from the FileStream
                        Template = BinaryData.FromString(class_name1.str2),

                        Parameters = BinaryData.FromString(class_name1.t2)
                    });

                    //  ArmOperation<WhatIfOperationResult> lro2 = await armDeployment.UpdateAsync(WaitUntil.Completed, content2);
                    var lro2 = await armDeployment.UpdateAsync(WaitUntil.Completed, content2);

                    Azure.Response response = lro2.GetRawResponse();

                    string Jsonresult = response.Content.ToString();

                   var json = JsonConvert.DeserializeObject(Jsonresult);

                    var print = json.ToString();

                    string msg = "Sucessfully deployed your resource in cloud!!";
                    
                    MessageBox.Show("Exception Message", msg, MessageBoxButton.OK,MessageBoxImage.Information);

                    //var con = new StringContent(class_name1.j_son, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = await client.PostAsync("https://prod-87.eastus.logic.azure.com:443/workflows/e9abf05bd431420f8ff3c624e3087bdb/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=MAiZcjO-nXlXrqRgDMkVRyoiSZR8Hf1ezyeg9-ctNCY", con);

                    //responsebody = await response.Content.ReadAsStringAsync();

                    //var body = responsebody.ToString();
                   

                    loader.Visibility = Visibility.Collapsed;

                  //  MessageBox.Show(body, msg, MessageBoxButton.OK, MessageBoxImage.Information);

                    MainWindow.Subcri_text.Clear();

                    MainWindow.cli_id_text.Clear();

                    MainWindow.cli_sec_txt.Clear();

                    Dep_name_text.Clear();

                    MainWindow.Tnt_txt.Clear();

                    drop.IsEnabled = true;
                    // drop.SelectedIndex = -1;

                    this.Close();

                    MainWindow.Show();

                    break;

                case MessageBoxResult.No:

                    Validate_btn.IsEnabled = false;

                    Deploy_btn.IsEnabled = true;

                    break;
            }
        }
     //   HttpClient client = new HttpClient();

        public string j_son, con;

        DataTable dt; DataRow row; public string responsebody;
        public async void Validate_button(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Resource_text.Text))
            {

                Validate_btn.IsEnabled = true;

                Deploy_btn.IsEnabled = true;

                MessageBox.Show("Please provide the resource group field");

            }
            else if (string.IsNullOrWhiteSpace(Template_text.Text))
            {
                MessageBox.Show("Please provide the template file field");

                Validate_btn.IsEnabled = true;

                Deploy_btn.IsEnabled = true;

            }
            else if (string.IsNullOrWhiteSpace(Parameter_text.Text))
            {
                MessageBox.Show("Please provide the parameter file field");

                Validate_btn.IsEnabled = true;

                Deploy_btn.IsEnabled = true;
            }
            else if (string.IsNullOrWhiteSpace(Dep_name_text.Text))
            {
                MessageBox.Show("Please provide the deployment name field");

                Validate_btn.IsEnabled = true;

                Deploy_btn.IsEnabled = true;
            }
            else if (drop.SelectedItem == null)
            {
                MessageBox.Show("Please provide the deployment mode field");
            }
            else
            {
                Guid guid = new Guid();

                Validate_btn.IsEnabled = false;

                Deploy_btn.IsEnabled = false;

                Back_btn.IsEnabled = false;

                Resource_text.IsEnabled = false;

                Tmp_browse_btn.IsEnabled = false;

                Para_browse_btn.IsEnabled = false;

                Template_text.IsEnabled = false;

                Parameter_text.IsEnabled = false;

                Dep_name_text.IsEnabled = false;

                drop.IsEnabled = false;

                try
                {
                    //    var obj = new
                    //    {
                    //        Subscription_id = class_name1.sub_id,

                    //        Client_id = class_name1.cli_id,

                    //        Client_Secret = class_name1.cli_sect,

                    //        Deployment_name = Dep_name_text.Text + guid.ToString().Substring(0, 8),

                    //        Tenant_id = class_name1.Ten_id,

                    //        Deployment_Mode = drop.SelectionBoxItem.ToString(),

                    //        Resource_Group_Name = Resource_text.Text,

                    //        Template_file = class_name1.str2,

                    //        Parameter_file = class_name1.str3,
                    //    };

                    //myPopupText.Visibility = Visibility.Visible;
                    loader.Visibility = Visibility.Visible;

                    //class_name1.j_son = JsonConvert.SerializeObject(obj);

                    //var con = new StringContent(class_name1.j_son, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = await client.PostAsync("https://prod-05.eastus.logic.azure.com:443/workflows/cae81b778a934e9cba2281a4965b2e99/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=tg7tgnWIIYu0gsnMwWdvsvquwy3BfOGzYtqnhsYOTM4", con);

                    //responsebody = await response.Content.ReadAsStringAsync();

                    //WhatifApiResult json2 = JsonConvert.DeserializeObject<WhatifApiResult>(responsebody);

                    //dynamic json = JsonConvert.DeserializeObject(responsebody);

                    //int coun = json2.properties.changes.Length;

                    class_name1.rsc_grp = Resource_text.Text;
                    class_name1.dep_name = Dep_name_text.Text + guid;
                    class_name1.dep_mode = drop.SelectedItem.ToString();

                    ClientSecretCredential clientSecretCredential = new ClientSecretCredential(

               clientId: class_name1.cli_id,

               tenantId: class_name1.Ten_id,

               clientSecret: class_name1.cli_sect);

                    // AccessToken accessToken = clientSecretCredential.GetTokenAsync(new TokenRequestContext(new[] { "https://management.azure.com/.default" })).Result;
                    //Console.WriteLine(accessToken);

                    TokenCredential credential = clientSecretCredential;

                    ArmClient client = new ArmClient(credential);

                    // this example assumes you already have this ArmDeploymentResource created on azure
                    // for more information of creating ArmDeploymentResource, please refer to the document of ArmDeploymentResource
                    string subscriptionId = class_name1.sub_id;

                    string resourceGroupName = class_name1.rsc_grp;

                    string scope = $"/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}";

                    string deploymentName = class_name1.dep_name;

                    ResourceIdentifier armDeploymentResourceId = ArmDeploymentResource.CreateResourceIdentifier(scope, deploymentName);

                    ArmDeploymentResource armDeployment = client.GetArmDeploymentResource(armDeploymentResourceId);

                    ArmDeploymentWhatIfContent content;
                    //   ArmDeploymentContent content2;

                    JObject template = JObject.Parse(class_name1.str3);

                    // Get the value of the parameters property as a JSON object
                    JObject parameters = (JObject)template["parameters"];

                    string t1 = class_name1.str2;

                    class_name1.t2 = parameters.ToString();
                    // open the template file as a FileStream

                    // assign the content variable inside the using block

                    string deploymentModeString = drop.SelectionBoxItem.ToString();

                    ArmDeploymentMode deploymentMode = (ArmDeploymentMode)Enum.Parse(typeof(ArmDeploymentMode), deploymentModeString);

                    content = new ArmDeploymentWhatIfContent(new ArmDeploymentWhatIfProperties(deploymentMode)
                    {
                        //content = new ArmDeploymentWhatIfContent(new ArmDeploymentWhatIfProperties(ArmDeploymentMode.Incremental)
                        //{
                        // use the FromStream method to create a BinaryData object from the FileStream
                        Template = BinaryData.FromString(class_name1.str2),

                        Parameters = BinaryData.FromString(class_name1.t2)
                    });

                    // use the content variable outside the using block
                    ArmOperation<WhatIfOperationResult> lro = await armDeployment.WhatIfAsync(WaitUntil.Completed, content);

                    Azure.Response response = lro.GetRawResponse();

                    string Jsonresult = response.Content.ToString();
                    
                    WhatifApiResult json2 = JsonConvert.DeserializeObject<WhatifApiResult>(Jsonresult);

                    dynamic json = JsonConvert.DeserializeObject(Jsonresult);

                    if (json.status=="Failed")
                    {
                        string str = json.error.message;

                        loader.Visibility = Visibility.Collapsed;

                        MessageBox.Show(str, "Exception Message", MessageBoxButton.OK, MessageBoxImage.Error);

                        string button = MessageBoxButton.OK.ToString();
                        if (button == "OK")
                        {
                            this.Close();
                        }
                    }
                    
                    else
                    {
                        int count = json2.properties.changes.Length;

                        //content2 = new ArmDeploymentContent(new ArmDeploymentProperties(ArmDeploymentMode.Incremental)
                        //   {
                        //    // use the FromStream method to create a BinaryData object from the FileStream
                        //    Template = BinaryData.FromString(class_name1.str2),
                        //    Parameters = BinaryData.FromString(class_name1.str3)
                        //});

                        //  ArmOperation<WhatIfOperationResult> lro2 = await armDeployment.UpdateAsync(WaitUntil.Completed, content2);
                        //   var lro2 = await armDeployment.UpdateAsync(WaitUntil.Completed, content2);                                  

                        dt = new DataTable("responsedata");

                        DataColumn dc1 = new DataColumn("Slno", typeof(int));

                        DataColumn dc2 = new DataColumn("ChangeType", typeof(string));

                        DataColumn dc3 = new DataColumn("ResourceName", typeof(string));

                        DataColumn dc4 = new DataColumn("ResourceType", typeof(string));

                        DataColumn dc5 = new DataColumn("Location", typeof(string));

                        dt.Columns.Add(dc1);

                        dt.Columns.Add(dc2);

                        dt.Columns.Add(dc3);

                        dt.Columns.Add(dc4);

                        dt.Columns.Add(dc5);

                        string del = "Delete", type2, type3, type4;

                        for (int i = 0; i < count; i++)
                        {
                            string type1 = json2.properties.changes[i].changeType;
                            if (type1 == del)
                            {
                                type2 = json2.properties.changes[i].before.name; ;
                                type3 = json2.properties.changes[i].before.type;
                                type4 = json2.properties.changes[i].before.location;
                            }
                            else
                            {
                                type2 = json2.properties.changes[i].after.name;
                                type3 = json2.properties.changes[i].after.type;
                                type4 = json2.properties.changes[i].after.location;
                            }
                            dataGrid1.Visibility = Visibility.Visible;

                            row = dt.NewRow();

                            row[0] = i + 1;
                            row[1] = type1;
                            row[2] = type2;
                            row[3] = type3;
                            row[4] = type4;
                            dt.Rows.Add(row);
                        }
                        // dataGrid1.Items.Clear();
                        dataGrid1.ItemsSource = dt.DefaultView;

                        // myPopupText.Visibility = Visibility.Collapsed;
                        loader.Visibility = Visibility.Collapsed;

                        Validate_btn.IsEnabled = false;

                        Deploy_btn.IsEnabled = true;

                        Back_btn.IsEnabled = true;

                        Resource_text.IsEnabled = true;

                        Tmp_browse_btn.IsEnabled = true;

                        Para_browse_btn.IsEnabled = true;

                        Template_text.IsEnabled = true;

                        Parameter_text.IsEnabled = true;

                        Dep_name_text.IsEnabled = true;

                        drop.IsEnabled = true;

                    }
                }
                catch (Exception ex)
                {

                    //loader.Visibility = Visibility.Collapsed;

                    //var body = responsebody.ToString();

                    loader.Visibility = Visibility.Collapsed;

                    string msg = ex.Message.ToString();

                    MessageBox.Show(ex.Message.ToString(), "Exception Message", MessageBoxButton.OK, MessageBoxImage.Error);

                    string value = MessageBoxButton.OK.ToString();

                    if (value == "OK")
                    {
                        this.Close();

                        // MainWindow.Show();
                    }
                    //this.Close();

                    //            this.Hide();

                    //            Validate_btn.IsEnabled = true;

                    //            Resource_text.IsEnabled = true;

                    //            Template_text.IsEnabled = true;

                    //            Parameter_text.IsEnabled = true;

                    //            Back_btn.IsEnabled = true;

                    //            Tmp_browse_btn.IsEnabled = true;

                    //            Para_browse_btn.IsEnabled = true;

                    //            Dep_name_text.IsEnabled = true;

                    //            drop.IsEnabled = true;
                    //            //   myPopupText.Visibility = Visibility.Collapsed;
                    //            loader.Visibility = Visibility.Collapsed;

                    //            MainWindow.Show();
                }
                }
        }
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Deploy_btn.IsEnabled = false;

            Validate_btn.IsEnabled = true;

            dataGrid1.Visibility = Visibility.Collapsed;
        }
        private void drop_SelectionChanged3(object sender, SelectionChangedEventArgs e)
        {
            Validate_btn.IsEnabled = true;

            Deploy_btn.IsEnabled = false;

            // window1.btnAdd8.IsEnabled = true;
            dataGrid1.Visibility = Visibility.Collapsed;

        }
        private void Back_button(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow.Show();

        }
    }
}