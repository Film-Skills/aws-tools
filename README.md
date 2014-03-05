aws-tools
=========

A C# class library that provides a friendly API for interacting with the [AWS API](http://aws.amazon.com/documentation/).

## How to Use
To invoke any of the methods in the library, the configuration information must be set. This allows you to set keys for each different method, as you may need to for security granularity with IAM. To set configuration information, run the `AWSTools.CONFIG.SetConfig()` method.

```
AWSTools.CONFIG.SetConfig("AWS ACCESS KEY", "AWS SECRET KEY", AWS Region Endpoint);
```

You must ensure the keys you are using have the correct IAM permissions for the services that you are requesting.

## S3
The Amazon Simple Storage Service can be accessed through several methods.

### PreSignUrl
Takes a key and a bucket and converts the path into a presigned url for access to restricted S3 content. [GetPreSignedUrlRequest](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3GetPreSignedUrlRequest_NET4_5.html)

- `bucket` - The name of the bucket where the key is stored
- `key` - The object key where the file is located on s3
- `expiresIn` - The number of minutes from now to expire the link in
- `protocol` - Whether to generate a HTTP or HTTPS link. In the form of an [Amazon.S3.Protocol](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3_Protocol_NET4_5.html)

### GetObject
Gets an object from S3 as a byte array for download or local/DB storage. [GetObjectRequest](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3GetObjectRequest_NET4_5.html).

- `bucket` - The name of the bucket where the key is stored
- `key` - The object key where the file is located on s3

### GetObjectResponse
Gets an object from S3 as a [GetObjectResponse](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3GetObjectResponse_NET4_5.html)

- `bucket` - The name of the bucket where the key is stored
- `key` - The object key where the file is located on s3

### ListObjects [4 Overloads]
Gets S3 objects in a bucket as a [`List<Amazon.S3.Model.S3Object>`](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3S3Object_NET4_5.html), using overload parameters to further filter the results. Useful for seeing all keys in a bucket or a sub directory of the bucket. [ListObjectsRequest](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3ListObjectsRequest_NET4_5.html)

- `bucket` - The bucket to get objects for.
- `prefix` - The prefix to use to search/filter objects. E.g. to get all objects in the bucket that start with `a` you would use `a` as the prefix.
- `delimiter` - The delimiter to use to further filter/restrict objects. If looking in the `a/` subfolder, use the `/` delimiter to get only keys in that subfolder and not any further nested keys.
- `marker` - The key to start with to get 1000 objects from.

### ListObjectsResponse
Gets S3 objects in a bucket as a [ListObjectsResponse](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3ListObjectsResponse_NET4_5.html). Uses the same params as ListObjects.

### PutObject [2 Overloads]
Uploads a file to an S3 bucket as a specified key and returns a [PutObjectResponse](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3PutObjectResponse_NET4_5.html). File can be uploaded from a FileStream or a Byte array. [PutObjectRequest](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3PutObjectRequest_NET4_5.html)

- `bucket` - The name of the bucket to upload the file to
- `key` - The key to store the object under on s3
- `stream` or `data` - The FileStream or Byte array of the file to upload to S3
- `storageClass` - The [`Amazon.S3.S3StorageClass`](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3_S3StorageClass_NET4_5.html) that the object is stored under (default is Standard)

### PutObjectResponse
Uploads a file to an S3 bucket as a specified key and returns a [PutObjectResponse](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3PutObjectResponse_NET4_5.html).

- `bucket` - The name of the bucket to upload the file to
- `key` - The key to store the object under on s3
- `stream` - The stream of the file to upload to S3
- `storageClass` - The [`Amazon.S3.S3StorageClass`](http://docs.aws.amazon.com/sdkfornet/latest/apidocs/items/TS3_S3StorageClass_NET4_5.html) that the object is stored under (default is Standard)
