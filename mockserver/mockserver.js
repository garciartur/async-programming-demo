var http = require('http');
var mockserver = require('mockserver');
 
http.createServer(mockserver(".\\mock-responses")).listen(9001);