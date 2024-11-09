﻿using AppoMobi.Specials;
using DrawnUi.Maui.Draw;
using ShadersCarousel.Models;

namespace ShadersCarouselDemo;

public class MockDataProvider
{

    public MockDataProvider()
    {
        RndExtensions.RandomizeTime();

        seed = RndExtensions.CreateRandom(0, 9999);
    }

    public void ResetIndex()
    {
        index = 0;
    }

    public void ResetIndexSmall()
    {
        indexSmall = 0;
    }

    int seed = 0;

    int seedSmall = 0;

    public List<SimpleItemViewModel> GetRandomItems(int count)
    {
        var items = new List<SimpleItemViewModel>();
        var preload = new List<string>();

        for (int i = 0; i < count; i++)
        {
            index++;

            var item = new SimpleItemViewModel
            {
                Id = (int)index,
                Title = GetRandomName(),
                Description = GetRandomDescription(),
                Banner = GetRandomImage(),
            };

            items.Add(item);
            preload.Add(item.Banner);
        }

        _ = SkiaImageManager.Instance.PreloadImages(preload);

        return items;
    }

    public List<SimpleItemViewModel> GetRandomSmallItems(int count)
    {
        var items = new List<SimpleItemViewModel>();

        for (int i = 0; i < count; i++)
        {
            indexSmall++;

            items.Add(new SimpleItemViewModel
            {
                Id = (int)indexSmall,
                Title = GetRandomName(),
                Description = GetRandomDescription(),
                Banner = GetRandomSmallImage(),
            });
        }

        return items;
    }

    public string GetRandomDescription()
    {
        return descriptions[random.Next(descriptions.Length)];
    }

    public string GetRandomName()
    {
        return NameGenerator.GenerateRandomName();
    }

    public string GetRandomImage()
    {
        seed++;
        return $"https://picsum.photos/600?random={seed}";
        //return $"https://picsum.photos/seed/{seed}/600/600";
    }

    public string GetRandomSmallImage()
    {
        seedSmall++;
        return $"https://picsum.photos/seed/{seedSmall}/300/300";
    }

    public string GetRandomAvatar()
    {
        return $"https://picsum.photos/seed/{seed}/200/200";
    }

    private static Random random = new Random();

    long index;
    long indexSmall;


    private static string[] descriptions = new string[]
    {
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
        "Vestibulum id ligula porta felis euismod semper.",
        "Donec id elit non mi porta gravida at eget metus.",
        "Nullam quis risus eget urna mollis ornare vel eu leo.",
        "Donec ullamcorper nulla non metus auctor fringilla."
    };

    private static string[] images = new string[]
    {
        "https://4.img-dpreview.com/files/p/TS1200x900~sample_galleries/7566437716/8187357568.jpg",
        "https://2.img-dpreview.com/files/p/TS1200x900~sample_galleries/7566437716/3825995157.jpg",
        "https://1.img-dpreview.com/files/p/TS1200x900~sample_galleries/7566437716/9281906178.jpg",
        "https://2.img-dpreview.com/files/p/TS1200x900~sample_galleries/7566437716/1548752695.jpg",
        "https://4.img-dpreview.com/files/p/TS1200x900~sample_galleries/7566437716/2659915629.jpg"
    };

    // 

    /// <summary>
    /// use NameGenerator.GenerateRandomName()
    /// </summary>
    public class NameGenerator
    {


        private static string[] firstNames = new string[]
        {
            "John", "Jane", "Peter", "Paul", "James", "Jill", "Soth", "Sara", "Robert", "Rachel"
        };

        private static string[] lastNames = new string[]
        {
            "Smith", "Bronson", "Williams", "Brown", "Jones", "Mall", "Davis", "Garcia", "Rodriguez", "Wilson"
        };

        public static string GenerateRandomName()
        {
            var firstName = firstNames[random.Next(firstNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];

            return firstName + " " + lastName;
        }
    }



}