using System;

namespace S3Uploder
{
    class Program
    {
        static void Main(string[] args)
        {

            //引数が足りない場合終了
            if (args.Length != 5)
                HelpAndExit();


            //パラメーター取得
            string IdentityPoolId = args[0];
            string RegionSystemName_backet = args[1];
            string backetName = args[2];
            string inputFileFullPath = args[3];
            string uploadS3path = args[4];


            try {
                //IdentityPoolIdからRegionSystemNameを抜き出す
                //us-east-1:77af42b6-0d85-458f-xxxx-xxxxxxxxのus-east-1のこと
                string RegionSystemName_IdentityPoolId = IdentityPoolId.Split(':')[0];

                //S3アップロード用クラスのインスタンス化
                s3upload s3 = new s3upload(IdentityPoolId, RegionSystemName_backet, backetName, RegionSystemName_IdentityPoolId);

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
                    //エラーコード1を出力して終了
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                //エラーメッセージ表示
                Console.WriteLine("Error: "+ex.Message);
                //エラーコード1を出力して終了
                Environment.Exit(1);
            }


            //正常終了時
            Environment.Exit(0);

        }


        /// <summary>
        /// Help表示して終了
        /// </summary>
        static void HelpAndExit()
        {
            string help = @"
S3Uploader.exe 

https://github.com/from2001/S3Uploader
S3Uploader.exe IdentityPoolId RegionSystemName backetName inputFileFullPath uploadS3path

IdentityPoolId       Identify Pool Id from https://console.aws.amazon.com/cognito/
RegionSystemName     RegionSystemName for Backet. us-east-1, ap-northeast-1, ap-southeast-1 etc.
backetName           S3 Backet name
inputFileFullPath    File or forlder path to upload 
uploadS3path         S3 path. (ie.  fol1/filename.ext for file upload)
                              (ie.  fol1/fol2/fol3/ for folder upload)
";
            Console.WriteLine(help);

            //エラーコード1を出力して終了
            Environment.Exit(2);

            
        }
    }
}
