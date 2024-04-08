using System.Globalization;
using System.Runtime.CompilerServices;
using Perceptron;

Console.WriteLine("Enter a:");
double a = double.Parse(Console.ReadLine());

Console.WriteLine("Enter the train-set file path:");
string fileName = Console.ReadLine();
var trainingData = ReadFile(fileName);

Console.WriteLine("Enter the test-set file path:");
fileName = Console.ReadLine();
var testData = ReadFile(fileName);

double[] weightVector = AssignRandomWeigths(4);
double t = GetPseudoDoubleWithinRange(-5, 5);
Dictionary<string, int> labelDictionary = AssignLabels(trainingData);

Train(a, trainingData, weightVector, t, labelDictionary);

foreach (var data in testData)
{
    TestRecord(data, weightVector, t, labelDictionary);
    data.Values.ToList().ForEach(i => Console.Write(i.ToString() + "; "));
    Console.WriteLine(" " + data.Label + " " + data.CalculatedLabel);
}

Console.WriteLine(String.Format("Accuracy: {0:P2}.", CalculateAccuracy(testData)));

while (true)
{
    Console.WriteLine("Enter values to classificate or write \"quit\" to exit (Use \";\" to separate values)");
    string message = Console.ReadLine();
    if (message == "quit")
    {
        System.Environment.Exit(0);
    }

    var elements = message.Split(";"); 
    var values = elements
        .Select(v => double.Parse(v.Replace(",","."), CultureInfo.InvariantCulture))
        .ToArray();
    Data data = new Data() { Values = values};
    data.Values.ToList().ForEach(i => Console.Write(i.ToString(CultureInfo.InvariantCulture) + "; "));
    TestRecord(data, weightVector, t, labelDictionary);
    Console.WriteLine(" " + data.CalculatedLabel);
}

static List<Data> ReadFile(string fileName)
{
    var lines = File.ReadLines(fileName);
    List<Data> result = new List<Data>();
    foreach (var line in lines)
    {
        var elements = line.Split(";");
        var label = elements.Last();
        var values = elements[..^1]
            .Select(v => double.Parse(v, CultureInfo.InvariantCulture))
            .ToArray();
        Data data = new Data() { Values = values, Label = label };
        result.Add(data);
    }

    return result;
}

static double[] AssignRandomWeigths(int length)
{
    double[] weigthVector = new double[length];
    for (int i = 0; i < length; i++)
    {
        weigthVector[i] = GetPseudoDoubleWithinRange(-5,5);
    }

    return weigthVector;
}

static double GetPseudoDoubleWithinRange(double lowerBound, double upperBound)
{
    var random = new Random();
    var rDouble = random.NextDouble();
    var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
    return rRangeDouble;
}

static Dictionary<string, int> AssignLabels(List<Data> trainingData)
{
    Dictionary<string, int> labelDictionary = new Dictionary<string, int>();
    int counter = 0;
    foreach (var data in trainingData)
    {
        if (!labelDictionary.ContainsKey(data.Label))
        {
            labelDictionary.Add(data.Label, counter++);
        }
    }
    return labelDictionary;
}

static void Train(double a, List<Data> trainingData, double[] weightVector, double t, Dictionary<string, int> labeldictionary)
{
    foreach (var data in trainingData)
    {
        int y = CalculateNET(data, weightVector, t);
        int d = 0;
        
        foreach (var entry in labeldictionary)
        {
            if (entry.Key == data.Label)
            {
                d = entry.Value;
            }
        }

        for (int i = 0; i < weightVector.Length; i++)
        {
            weightVector[i] += (d - y) * a * data.Values.ElementAt(i);
        }

        t -= (d - y) * a;
    }
}

static int CalculateNET(Data data, double[] weightVector, double t)
{
    double net = 0;
    for (int i = 0; i < data.Values.Length; i++)
    {
        net += data.Values[i] * weightVector[i];
    }
    net -= t;
    
    if (net < 0)
    {
        return 0;
    }
    return 1;
}

static void TestRecord(Data data, double[] weightVector, double t, Dictionary<string, int> labelDictionary)
{
    int y = CalculateNET(data, weightVector, t);
            
    foreach (var entry in labelDictionary)
    {
        if (entry.Value == y)
        {
            data.CalculatedLabel = entry.Key;
        }
    }
}

static double CalculateAccuracy(List<Data> testData)
{
    double correct = 0;
    foreach (var record in testData)
    {
        if (record.Label == record.CalculatedLabel)
        {
            correct++;
        }
    }

    return correct / testData.Count;
}

