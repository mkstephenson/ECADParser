using Common;
using Common.Models.Data;
using System.Data;
using System.Diagnostics;
using System.Text.Json;

var configDoc = JsonDocument.Parse(File.ReadAllText("config.json"));
var locationsConfig = JsonSerializer.Deserialize<Dictionary<string, string>>(configDoc.RootElement.GetProperty("DataLocations"));
var parallelTasks = configDoc.RootElement.GetProperty("Concurrency").GetInt32();
var jsonSerializerConfigs = new JsonSerializerOptions { WriteIndented = false };

var tasks = new List<Task>();
var concurrency = new SemaphoreSlim(parallelTasks);
var allFileCount = locationsConfig.Values.Select(p => Directory.EnumerateFiles(p, "*.txt").Count()).Sum();
var processedCount = 0;

/*
 * Cloud Cover
 */
if (locationsConfig.ContainsKey("CC"))
{
  var ccFolder = ProcessCommon("CC");
  foreach (var file in Directory.EnumerateFiles(ccFolder).Where(f => Path.GetFileName(f).StartsWith("CC") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<CC>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new CC(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}

/*
 * Wind Direction
 */
if (locationsConfig.ContainsKey("DD"))
{
  var ddFolder = ProcessCommon("DD");
  foreach (var file in Directory.EnumerateFiles(ddFolder).Where(f => Path.GetFileName(f).StartsWith("DD") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<DD>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new DD(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}


/*
 * Wind Speed
 */
if (locationsConfig.ContainsKey("FG"))
{
  var fgFolder = ProcessCommon("FG");
  foreach (var file in Directory.EnumerateFiles(fgFolder).Where(f => Path.GetFileName(f).StartsWith("FG") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<FG>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new FG(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  }
}

/*
 * Wind Gust
 */
if (locationsConfig.ContainsKey("FX"))
{
  var fxFolder = ProcessCommon("FX");
  foreach (var file in Directory.EnumerateFiles(fxFolder).Where(f => Path.GetFileName(f).StartsWith("FX") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<FX>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new FX(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  }
}

/*
 * Humidity
 */
if (locationsConfig.ContainsKey("HU"))
{
  var huFolder = ProcessCommon("HU");
  foreach (var file in Directory.EnumerateFiles(huFolder).Where(f => Path.GetFileName(f).StartsWith("HU") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<HU>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new HU(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}

/*
 * Sea Level Pressure
 */
if (locationsConfig.ContainsKey("PP"))
{
  var ppFolder = ProcessCommon("PP");
  foreach (var file in Directory.EnumerateFiles(ppFolder).Where(f => Path.GetFileName(f).StartsWith("PP") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<PP>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new PP(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}


/*
 * Global Radiation
 */
if (locationsConfig.ContainsKey("QQ"))
{
  var qqFolder = ProcessCommon("QQ");
  foreach (var file in Directory.EnumerateFiles(qqFolder).Where(f => Path.GetFileName(f).StartsWith("QQ") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<QQ>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new QQ(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}


/*
 * Precipitation
 */
if (locationsConfig.ContainsKey("RR"))
{
  var rrFolder = ProcessCommon("RR");
  foreach (var file in Directory.EnumerateFiles(rrFolder).Where(f => Path.GetFileName(f).StartsWith("RR") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<RR>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new RR(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  }
}


/*
 * Snow Depth
 */
if (locationsConfig.ContainsKey("SD"))
{
  var sdFolder = ProcessCommon("SD");
  foreach (var file in Directory.EnumerateFiles(sdFolder).Where(f => Path.GetFileName(f).StartsWith("SD") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<SD>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new SD(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}

/*
 * Sunshine
 */
if (locationsConfig.ContainsKey("SS"))
{
  var ssFolder = ProcessCommon("SS");
  foreach (var file in Directory.EnumerateFiles(ssFolder).Where(f => Path.GetFileName(f).StartsWith("SS") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<SS>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new SS(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}

/*
 * Mean Temperature
 */
if (locationsConfig.ContainsKey("TG"))
{
  var tgFolder = ProcessCommon("TG");
  foreach (var file in Directory.EnumerateFiles(tgFolder).Where(f => Path.GetFileName(f).StartsWith("TG") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<TG>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new TG(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}

/*
 * Minimum Temperature
 */
if (locationsConfig.ContainsKey("TN"))
{
  var tnFolder = ProcessCommon("TN");
  foreach (var file in Directory.EnumerateFiles(tnFolder).Where(f => Path.GetFileName(f).StartsWith("TN") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<TN>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new TN(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}

/*
 * Maximum Temperature
 */
if (locationsConfig.ContainsKey("TX"))
{
  var txFolder = ProcessCommon("TX");
  foreach (var file in Directory.EnumerateFiles(txFolder).Where(f => Path.GetFileName(f).StartsWith("TX") && Path.GetExtension(f) == ".txt"))
  {
    concurrency.Wait();
    tasks.Add(Task.Run(async () =>
    {
      try
      {
        System.Diagnostics.Stopwatch stopwatch = Stopwatch.StartNew();
        var table = await TableHelpers.ParseTableAsync(file);
        var items = new List<TX>();
        foreach (DataRow row in table.Rows)
        {
          items.Add(new TX(row));
        }
        using var fileStream = File.Create(Path.ChangeExtension(file, ".json"));
        await JsonSerializer.SerializeAsync(fileStream, items, jsonSerializerConfigs);
        stopwatch.Stop();
        Interlocked.Increment(ref processedCount);
        global::System.Console.WriteLine($"Processing file {processedCount}/{allFileCount} ({file}) in {stopwatch.Elapsed}");
      }
      finally
      {
        concurrency.Release();
      }
    }));
  } 
}

Task.WaitAll(tasks.ToArray());

Console.WriteLine("Finished parsing values");

/*
 * Helper Methods
 */
string ProcessCommon(string typeName)
{
  var folder = locationsConfig[typeName];
  Console.WriteLine($"Parsing files in folder {folder}");
  Console.WriteLine("Adding metadata");
  var stations = TableHelpers.GetStations(folder);
  File.WriteAllText(Path.Combine(folder, $"{typeName}_stations.json"), JsonSerializer.Serialize(stations, jsonSerializerConfigs));
  var sources = TableHelpers.GetSources(folder);
  File.WriteAllText(Path.Combine(folder, $"{typeName}_sources.json"), JsonSerializer.Serialize(sources, jsonSerializerConfigs));
  var elements = TableHelpers.GetElements(folder);
  File.WriteAllText(Path.Combine(folder, $"{typeName}_elements.json"), JsonSerializer.Serialize(elements, jsonSerializerConfigs));

  return folder;
}
