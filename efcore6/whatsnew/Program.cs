using BenchmarkDotNet.Running;
using whatsnew;

var summary = BenchmarkRunner.Run<Benchmark>();

//var bench = new Benchmark();

//bench.QueryWithLiteralsAndScan();

//bench.QueryWithParameterAndNoScan();

//bench.QueryWithClientSideProcessing();

//bench.QueryCompiled();

//bench.InsertWithBatches();

