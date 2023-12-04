// See https://aka.ms/new-console-template for more information
using AdventSolutions.Solutions;

var day = int.Parse(args[0]);
var part = int.Parse(args[1]);
var inputPath = args[2];

if (day < 1) throw new IndexOutOfRangeException("Day must be greater than 0");
if (part < 1 || part > 2) throw new IndexOutOfRangeException("Part must be 1 or 2");
if (!File.Exists(inputPath)) throw new FileNotFoundException("Input file not found", inputPath);

Solver.Solve(day, part, inputPath);
