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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_whatifSample2
{
    class validate
    {
       //public static Class1 class_name1 = new Class1();
       // public static async Task Main(string[] args)
       // {
           
       //     ClientSecretCredential clientSecretCredential = new ClientSecretCredential(
       //clientId: class_name1.cli_id,

       //tenantId: class_name1.Ten_id,

       //clientSecret: class_name1.cli_sect);

       //     // AccessToken accessToken = clientSecretCredential.GetTokenAsync(new TokenRequestContext(new[] { "https://management.azure.com/.default" })).Result;
       //     //Console.WriteLine(accessToken);

       //     TokenCredential credential = clientSecretCredential;

       //     ArmClient client = new ArmClient(credential);

       //     // this example assumes you already have this ArmDeploymentResource created on azure
       //     // for more information of creating ArmDeploymentResource, please refer to the document of ArmDeploymentResource
       //     string subscriptionId = class_name1.sub_id;

       //     string resourceGroupName = class_name1.rsc_grp;

       //     string scope = $"/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}";

       //     string deploymentName = class_name1.dep_name;

       //     ResourceIdentifier armDeploymentResourceId = ArmDeploymentResource.CreateResourceIdentifier(scope, deploymentName);

       //     ArmDeploymentResource armDeployment = client.GetArmDeploymentResource(armDeploymentResourceId);

       //     ArmDeploymentWhatIfContent content;
       //     //   ArmDeploymentContent content2;

       //     JObject template = JObject.Parse(class_name1.str3);

       //     // Get the value of the parameters property as a JSON object
       //     JObject parameters = (JObject)template["parameters"];

       //     string t1 = class_name1.str2;

       //     class_name1.t2 = parameters.ToString();
       //     // open the template file as a FileStream

       //     // assign the content variable inside the using block

       //     string deploymentModeString = "Incremental";

       //     ArmDeploymentMode deploymentMode = (ArmDeploymentMode)Enum.Parse(typeof(ArmDeploymentMode), deploymentModeString);

       //     content = new ArmDeploymentWhatIfContent(new ArmDeploymentWhatIfProperties(deploymentMode)
       //     {
       //         //content = new ArmDeploymentWhatIfContent(new ArmDeploymentWhatIfProperties(ArmDeploymentMode.Incremental)
       //         //{
       //         // use the FromStream method to create a BinaryData object from the FileStream
       //         Template = BinaryData.FromString(class_name1.str2),

       //         Parameters = BinaryData.FromString(class_name1.t2)
       //     });

       //     // use the content variable outside the using block
       //     ArmOperation<WhatIfOperationResult> lro = await armDeployment.WhatIfAsync(WaitUntil.Completed, content);

       //     Azure.Response response = lro.GetRawResponse();

       //     string Jsonresult = response.Content.ToString();

       //     WhatifApiResult json2 = JsonConvert.DeserializeObject<WhatifApiResult>(Jsonresult);

       //     dynamic json = JsonConvert.DeserializeObject(Jsonresult);

       //     int count = json2.properties.changes.Length;
       // }
    }
}
