using System;

namespace S3Uploder
{
    class Program
    {
        static void Main(string[] args)
        {

            //引数が足りない場合終了
            if (args.Length != 4)
                HelpAndExit();
            //パラメーター取得
            string IdentityPoolId = args[0];
            string backetName = args[1];
            string inputFileFullPath = args[2];
            string uploadS3path = args[3];

            try { 
                //S3アップロード用クラスのインスタンス化
                s3upload s3 = new s3upload(IdentityPoolId, backetName);

                if (System.IO.File.Exists(inputFileFullPath))
                {
                    //ファイル指定の場合
                    s3.uploadFileToS3(inputFileFullPath, uploadS3path);
                }
                else if (System.IO.Directory.Exists(inputFileFullPath))
                {
                    //フォルダ指定の場合
                    s3.uploadFolderToS3(inputFileFullPath, uploadS3path);
                }
                else
                {
                    //指定間違いの場合
                    Console.WriteLine("File path doesn't exist");
                    HelpAndExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: "+ex.Message);
                HelpAndExit();
            }

        }

        /// <summary>
        /// Help表示して終了
        /// </summary>
        static void HelpAndExit()
        {
            string help = @"

S3Uploader.exe 

https://github.com/from2001/S3Uploader
S3Uploader.exe IdentityPoolId backetName inputFileFullPath uploadS3path
IdentityPoolId       Identify Pool Id from https://console.aws.amazon.com/cognito/
backetName           S3 Backet name
inputFileFullPath    File or forlder path to upload 
uploadS3path         S3 path. (ie.  fol1/filename.ext for file upload)
                              (ie.  fol1/fol2/fol3/ for folder upload)
";
            Console.WriteLine(help);
            Environment.Exit(0);

            
        }
    }
}
