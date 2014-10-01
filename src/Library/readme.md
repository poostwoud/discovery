Example of usage:

'''c#
var protocol = new Library.Protocol.HTTPProtocol();
var response = protocol.Read(new Uri(@"http://haltalk.herokuapp.com/"));
Console.Write(response.Result);
'''
