# S3Uploader
Command line interface for AWS S3 file uploader
Developed in Microsoft Visual C# 2015

    S3Uploader.exe
    
    https://github.com/from2001/S3Uploader
    S3Uploader.exe IdentityPoolId backetName inputFileFullPath uploadS3path
    
    IdentityPoolId       Identify Pool Id from https://console.aws.amazon.com/cognito/
    RegionSystemName     RegionSystemName for Backet. us-east-1, ap-northeast-1, ap-southeast-1 etc.
    backetName           S3 Backet name
    inputFileFullPath    File or forlder path to upload
    uploadS3path         S3 path. (ie.  fol1/filename.ext for file upload)
                                  (ie.  fol1/fol2/fol3/ for folder upload)


[How to generate IdentityPoolId in Japanese](http://psychic-vr-lab.com/blog/unity/unity%E3%81%8B%E3%82%89c%E3%81%A7amazon-web-service%E3%81%AEs3%E3%82%B9%E3%83%88%E3%83%AC%E3%83%BC%E3%82%B8%E3%81%AB%E3%82%A2%E3%83%83%E3%83%97%E3%83%AD%E3%83%BC%E3%83%89%E3%81%99%E3%82%8B%E3%83%A1/)

S3Uploader.exe works on both WIndows and Linux with Mono which requires755 permission on Linux to execute.

