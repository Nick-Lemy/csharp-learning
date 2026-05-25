
// var dm = new Dinglemouse().SetName("Bob").SetAge(27).SetSex('M');
// var expected = "Hello. My name is Bob. I am 27. I am male.";

var dm = new Dinglemouse().SetAge(25).SetSex('F').SetAge(30).SetName("Betty");
var expected = "Hello. I am 30. My name is Betty";

Console.WriteLine($"\nExpected: \"{expected}\"\ngot \"{dm.Hello()}\"");