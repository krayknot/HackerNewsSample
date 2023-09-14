using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Collabra_Test.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;

namespace Collabra_Test.Services
{
    public class HackerNewsApi
    {
        private readonly RestClient _restClient;

        public HackerNewsApi()
        {
            _restClient = new RestClient("https://hacker-news.firebaseio.com/v0");
        }

        public async Task<List<HackerNewsStory>> GetBestStories(int storyCount = 10)
        {
            List<int> bestStories = await GetBestStoryIdsAsync(storyCount);
            return await GetHackerNewsStoriesAsync(bestStories);
        }

        public async Task<List<int>> GetBestStoryIdsAsync(int storyCount)
        {
            try
            {
                var request = new RestRequest("beststories.json", Method.Get);
                var response = await _restClient.ExecuteAsync<List<int>>(request);

                if (response.IsSuccessful)
                {
                    var storyIds = response.Data.Take(storyCount).ToList();
                    return storyIds;
                }
                else
                {
                    Console.WriteLine($"An error occurred while fetching data: {response.ErrorMessage}");
                    return new List<int>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching data: {ex.Message}");
                return new List<int>();
            }
        }

        public async Task<List<HackerNewsStory>> GetHackerNewsStoriesAsync(List<int> storyIds)
        {
            var stories = new List<HackerNewsStory>();

            foreach (var storyId in storyIds)
            {
                var storyUrl = $"item/{storyId}.json";

                try
                {
                    var request = new RestRequest(storyUrl, Method.Get);
                    var response = await _restClient.ExecuteAsync<HackerNewsStory>(request);

                    if (response.IsSuccessful)
                    {
                        stories.Add(response.Data);
                    }
                    else
                    {
                        Console.WriteLine($"An error occurred while fetching data for story {storyId}: {response.ErrorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while fetching data: {ex.Message}");
                }
            }

            // Score sorting - Descending order
            stories.Sort((a, b) => b.score.CompareTo(a.score));
            return stories;
        }
    }

}


