using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WOLAS.AWS
{
    public static class SQS
    {
        /// <summary>
        /// Sends a new message to Amazon SQS using
        /// the specified queue URL. The message size
        /// limit is 256kb.
        /// </summary>
        /// <param name="queueUrl">The sqs queue endpoint to send the message to.</param>
        /// <param name="message">The message to send via SQS.</param>
        public static Amazon.SQS.Model.SendMessageResponse Send(string queueUrl, string message)
        {
            // Check that there is content for the message.
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("The message cannot be nothing!");

            // Check that the size of the message is okay.
            int messageSize = System.Text.ASCIIEncoding.Unicode.GetByteCount(message);
            if ((messageSize / 1000) >= 256)
                throw new ArgumentException("The message size cannot be larger than 256kb!");

            // Send the message to Amazon.
            Amazon.SQS.Model.SendMessageResponse response = new Amazon.SQS.Model.SendMessageResponse();
            using (Amazon.SQS.IAmazonSQS client = new Factory().SQSClient())
            {
                Amazon.SQS.Model.SendMessageRequest request = new Amazon.SQS.Model.SendMessageRequest()
                {
                    MessageBody = message,
                    QueueUrl = queueUrl
                };

                response = client.SendMessage(request);
            }
            return response;
        }

        /// <summary>
        /// Gets messages from an SQS queue.
        /// </summary>
        /// <param name="queueUrl">The sqs queue endpoint to get the messages from.</param>
        /// <param name="maxMessages">The maximum number of messages to return (max 10)</param>
        /// <param name="visibilityTimeout">The number of minutes to hide the message from other consumers for (max 12 hours or 720 minutes)</param>
        /// <returns></returns>
        public static Amazon.SQS.Model.ReceiveMessageResponse ReceiveMessages(string queueUrl, int maxMessages = 10, int visibilityTimeout = 0)
        {
            // Check for the maximum number of messages to get.
            if (maxMessages > 10)
                throw new ArgumentException("The maximum number of messages that can be returned is 10");

            // Check that the visibility timeout is okay.
            if (visibilityTimeout > 720)
                throw new ArgumentException("The maximum amount of time that the message can be hidden for is 12 hours.");

            // Get the messages from the queue.
            Amazon.SQS.Model.ReceiveMessageResponse response = new Amazon.SQS.Model.ReceiveMessageResponse();
            using (Amazon.SQS.IAmazonSQS client = new Factory().SQSClient())
            {
                Amazon.SQS.Model.ReceiveMessageRequest request = new Amazon.SQS.Model.ReceiveMessageRequest()
                {
                    QueueUrl = queueUrl,
                    MaxNumberOfMessages = maxMessages
                };

                if (visibilityTimeout > 0)
                {
                    request.VisibilityTimeout = visibilityTimeout;
                }

                response = client.ReceiveMessage(request);
            }
            return response;
        }

        /// <summary>
        /// Deletes a message from an SQS queue.
        /// </summary>
        /// <param name="queueUrl">The sqs queue endpoint to delete the message from.</param>
        /// <param name="receiptHandle">The unique receipt handle for the message.</param>
        public static Amazon.SQS.Model.DeleteMessageResponse Delete(string queueUrl, string receiptHandle)
        {
            Amazon.SQS.Model.DeleteMessageResponse response = new Amazon.SQS.Model.DeleteMessageResponse();
            using (Amazon.SQS.IAmazonSQS client = new Factory().SQSClient())
            {
                Amazon.SQS.Model.DeleteMessageRequest request = new Amazon.SQS.Model.DeleteMessageRequest()
                {
                    QueueUrl = queueUrl,
                    ReceiptHandle = receiptHandle
                };

                response = client.DeleteMessage(request);
            }
            return response;
        }
    }
}
