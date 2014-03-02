aws-tools
=========

A C# class library that provides a friendly API for interacting with the (http://aws.amazon.com/documentation/)[AWS API].

## How to Use
To invoke any of the methods in the library, the configuration information must be set. This allows you to set keys for each different method, as you may need to for security granularity with IAM. To set configuration information, run the `AWSTools.CONFIG.SetConfig()` method.

```
AWSTools.CONFIG.SetConfig("AWS ACCESS KEY", "AWS SECRET KEY", AWS Region Endpoint);
```

You must ensure the keys you are using have the correct IAM permissions for the services that you are requesting.

## S3
The Amazon Simple Storage Service can be accessed through several methods.

### PreSignUrl
Takes a key and a bucket and converts the path into a presigned url for access to restricted S3 content.

- `bucket` - The name of the bucket where the key is stored
- `key` - The object key where the file is located on s3
- `expiresIn` - The number of minutes from now to expire the link in
- `protocol - Whether to generate a HTTP or HTTPS link. In the form of an (http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3_Protocol_NET4_5.html)[Amazon.S3.Protocol]