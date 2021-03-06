﻿using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace TextAnalytics.Services
{
    public class SentimentService
    {
        //Example Base Url: https://westus.api.cognitive.microsoft.com
        const string _sentimentAPIBaseUrl = "https://brazilsouth.api.cognitive.microsoft.com";//"<Base URL API>";
        const string _textSentimentAPIKey = "b87a3ba42e9141a8a8cc2bbe4c0541e6";//"<API Key>";
        //HttpClient wrapper for accessing the TextAnalytics API
        readonly static TextAnalyticsClient _textAnalyticsApiClient
            = new TextAnalyticsClient(new ApiKeyServiceClientCredentials(_textSentimentAPIKey))
            {
                Endpoint = _sentimentAPIBaseUrl
            };
        //Input the text to be analyzed and return its sentiment score. 
        //The sentiment score will range from 0 to 1, where 0 is negative sentiment and 1 is positive sentiment.
        public async Task<double?> GetSentiment(string text)
        {
            //Create the request object to send to the TextAnalytics API
            //The request can contain multiple text inputs which you can use to batch multiple requests into one API call
            var request = new MultiLanguageBatchInput(new List<MultiLanguageInput>
            {
                { new MultiLanguageInput(id: "1", text: text) }
            });
            //Get the sentiment results from the TextAnalytics API 
            var sentimentResult = await _textAnalyticsApiClient.SentimentAsync(null,request);
            //Parse the sentiment score 
            return sentimentResult?.Documents?.FirstOrDefault()?.Score;
        }
        //Helper class to add our API Key to the HttpRequestMessage Header
        class ApiKeyServiceClientCredentials : ServiceClientCredentials
        {
            readonly string _subscriptionKey;
            public ApiKeyServiceClientCredentials(string subscriptionKey)
            {
                _subscriptionKey = subscriptionKey;
            }
            public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));
                request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                return Task.CompletedTask;
            }
        }
    }
}
