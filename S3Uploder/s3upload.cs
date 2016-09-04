using System;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using Amazon.CognitoIdentity;
using Amazon;


public class s3upload
{

    private CognitoAWSCredentials credentials;
    private string backetNameString;
    private RegionEndpoint regionVal;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="IdentityPoolId">CognitoのIdentify Pool ID</param>
    /// <param name="backetName">バケット名</param>
    /// <param name="region">Region未指定の場合はUSEast1</param>
    public s3upload(string IdentityPoolId, string backetName, RegionEndpoint region = null)
    {
        if (region == null) { regionVal = RegionEndpoint.USEast1; }    //Region未指定の場合はUSEast1を設定
        credentials = new CognitoAWSCredentials(IdentityPoolId, regionVal);
        backetNameString = backetName;
    }


    /// <summary>
    /// 指定ファイルをS3バケットにアップロードします
    /// </summary>
    /// <param name="inputFileFullPath">アップロードするローカルファイルパス</param>
    /// <param name="uploadS3path">S3パス。fol/filenameと指定するとfolフォルダ以下にアップロードする</param>
    public void uploadFileToS3(string inputFileFullPath, string uploadS3path)
    {
        AmazonS3Client S3Client = new AmazonS3Client(credentials, regionVal);

        //ファイル読み込み
        var stream = new FileStream(inputFileFullPath,
            FileMode.Open, FileAccess.Read, FileShare.Read);

        //リクエスト作成
        var request = new PostObjectRequest()
        {
            Bucket = backetNameString,
            Key = uploadS3path,
            InputStream = stream,
            CannedACL = S3CannedACL.Private
        };

        //アップロード
        S3Client.PostObjectAsync(request, (responseObj) =>
        {
            if (responseObj.Exception == null)
            {
                //Success
                Debug.Log(uploadS3path + "   :Upload successed");
            }
            else { Debug.LogError(string.Format("\n receieved error {0}", responseObj.Response.HttpStatusCode.ToString())); }
        });
    }
}
