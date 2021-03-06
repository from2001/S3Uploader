﻿using System;
using Amazon.S3;
using Amazon.CognitoIdentity;
using Amazon;
using Amazon.S3.Transfer;

public class s3upload
{
    private CognitoAWSCredentials credentials;
    private string g_backetNameString;
    private string g_RegionSystemName_backet;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="IdentityPoolId">CognitoのIdentify Pool ID</param>
    /// <param name="backetName">バケット名</param>
    /// <param name="RegionSystemName"> us-west-1, ap-northeast-1, ap-south-1 etc..</param>
    public s3upload(string IdentityPoolId, string RegionSystemName_backet, string backetName, string RegionSystemName_IdentityPoolId)
    {
        RegionEndpoint regionVal = RegionEndpoint.GetBySystemName(RegionSystemName_IdentityPoolId);
        credentials = new CognitoAWSCredentials(IdentityPoolId, regionVal);
        g_backetNameString = backetName;
        g_RegionSystemName_backet = RegionSystemName_backet;
    }

    /// <summary>
    /// 指定ファイルをS3バケットにアップロードします
    /// </summary>
    /// <param name="inputFileFullPath">アップロードするローカルファイルパス</param>
    /// <param name="uploadS3path">S3パス。fol/filenameと指定するとfolフォルダ以下にアップロードする</param>
    public void uploadFileToS3(string inputFileFullPath, string uploadS3path)
    {
        RegionEndpoint Region_backet = RegionEndpoint.GetBySystemName(g_RegionSystemName_backet);
        AmazonS3Client S3Client = new AmazonS3Client(credentials, Region_backet);
        TransferUtility fileTransferUtility = new TransferUtility(S3Client);

        //ファイル転送
        fileTransferUtility.Upload(inputFileFullPath, g_backetNameString, uploadS3path);
        Console.WriteLine("Uploaded: " + uploadS3path);

    }

    /// <summary>
    /// 指定フォルダをS3バケットにアップロードします
    /// </summary>
    /// <param name="inputFileFullPath">アップロードするローカルフォルダパス</param>
    /// <param name="uploadS3dir">S3パス。fol/と指定するとfolフォルダ以下にアップロードする</param>
    public void uploadFolderToS3(string inputFolderFullPath, string uploadS3dir)
    {
        //uploadS3pathの末尾が/でない場合は/を付加
        if (uploadS3dir.EndsWith("/")==false) { uploadS3dir = uploadS3dir + "/"; }
        //inputFolderFullPathの末尾が\でない場合は\を付加
        if (inputFolderFullPath.EndsWith("\\") == false) { inputFolderFullPath = inputFolderFullPath + "\\"; }

        //ファイル一覧取得
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(inputFolderFullPath);
        System.IO.FileInfo[] files =
            di.GetFiles("*", System.IO.SearchOption.AllDirectories);

        //１ファイルずつアップロード
        foreach (System.IO.FileInfo f in files)
        {
            string uploadS3path;
            uploadS3path = uploadS3dir + f.FullName.Replace(inputFolderFullPath, "");
            uploadS3path = uploadS3path.Replace(@"\", "/");
            uploadFileToS3(f.FullName, uploadS3path);
        }


    }



}
