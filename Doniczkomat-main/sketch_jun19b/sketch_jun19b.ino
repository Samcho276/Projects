#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WiFiMulti.h> 
#include <ESP8266mDNS.h>
#include <ESP8266WebServer.h>

int dry = 700;
int wet = 500;
int soilMoisture = 10;
int waterLevel = 10;
String potName = "Noname";
int pumpingTime = 10000;
ESP8266WiFiMulti wifiMulti;     // Create an instance of the ESP8266WiFiMulti class, called 'wifiMulti'

ESP8266WebServer server(80);    // Create a webserver object that listens for HTTP request on port 80

const int led = 2;

void handleRoot();              // function prototypes for HTTP handlers
void handleLED();
void handleNotFound();

void setup(void){
  Serial.begin(115200);         // Start the Serial communication to send messages to the computer
  delay(10);
  Serial.println('\n');

  pinMode(led, OUTPUT);

  pinMode(D1, OUTPUT);
  pinMode(D2, OUTPUT);
  digitalWrite(D2, HIGH);

  wifiMulti.addAP("Zwierzak 2.4", "Kr0licz3K");   // add Wi-Fi networks you want to connect to
  wifiMulti.addAP("SamoLaptop", "12345678");
  wifiMulti.addAP("ssid_from_AP_3", "your_password_for_AP_3");

  Serial.println("Connecting ...");
  int i = 0;
  while (wifiMulti.run() != WL_CONNECTED) { // Wait for the Wi-Fi to connect: scan for Wi-Fi networks, and connect to the strongest of the networks above
    delay(250);
    Serial.print('.');
  }
  Serial.println('\n');
  Serial.print("Connected to ");
  Serial.println(WiFi.SSID());              // Tell us what network we're connected to
  Serial.print("IP address:\t");
  Serial.println(WiFi.localIP());           // Send the IP address of the ESP8266 to the computer

  if (MDNS.begin("esp8266")) {              // Start the mDNS responder for esp8266.local
    Serial.println("mDNS responder started");
  } else {
    Serial.println("Error setting up MDNS responder!");
  }

  server.on("/", HTTP_GET, handleRoot);     // Call the 'handleRoot' function when a client requests URI "/"
  server.on("/OMG", HTTP_GET, handleOMG);
  server.on("/STATUS", HTTP_GET, handleSTATUS);
  server.on("/LED", HTTP_POST, handleLED);  // Call the 'handleLED' function when a POST request is made to URI "/LED"
  server.on("/PUMP", HTTP_POST, handlePUMP);  // Call the 'handlePUMP' function when a POST request is made to URI "/PUMP"
  server.onNotFound(handleNotFound);        // When a client requests an unknown URI (i.e. something other than "/"), call function "handleNotFound"

  server.begin();                           // Actually start the server
  Serial.println("HTTP server started");
}

void loop(void){
  server.handleClient();                    // Listen for HTTP requests from clients
}
void handleOMG() {                         // When URI / is requested, send a web page with a button to toggle the LED
  server.send(200, "text/plain", "ALOHOMORA");
}
void handleLED() {                          // If a POST request is made to URI /LED
  digitalWrite(led,!digitalRead(led));      // Change the state of the LED
  server.sendHeader("Location","/");        // Add a header to respond with a new location for the browser to go to the home page again
  server.send(303);                         // Send it back to the browser with an HTTP status 303 (See Other) to redirect
}
void handleRoot() {                         // When URI / is requested, send a web page with a button to toggle the LED
  server.send(200, "text/html", 
  String("<form action=\"/LED\" method=\"POST\"><input type=\"submit\" value=\"Toggle LED\"></form>")+
  String("<form action=\"/PUMP\" method=\"POST\"><input type=\"submit\" value=\"PUMP\"></form>")+
  String("<form action=\"/OMG\" method=\"GET\"><input type=\"submit\" value=\"OMG\"></form>")+
  String("<form action=\"/STATUS\" method=\"GET\"><input type=\"submit\" value=\"STATUS\"></form>")
  );
}
void handlePUMP() {                          // If a POST request is made to URI /LED
  server.sendHeader("Location","/");        // Add a header to respond with a new location for the browser to go to the home page again
  server.send(303);                         // Send it back to the browser with an HTTP status 303 (See Other) to redirect
  PumpWater();
}
void handleSTATUS() {                         // When URI / is requested, send a web page with a button to toggle the STATUS
  ReadHumidity();
  String stats = 
  String("{")+
  String("\"name\": \"")+String(potName)+String("\"")+
  String(",\"waterLevel\": ")+String(waterLevel)+
  String(",\"soilMoisture\": ")+String(soilMoisture)+
  String("}");
/*
{
\"id\": ,
\"waterLevel\": , 
\"soilMoisture\": 
}
*/
  server.send(200, "text/plain", stats);
}
void handleNotFound(){
  server.send(404, "text/plain", "404: Not found"); // Send HTTP status 404 (Not Found) when there's no handler for the URI in the request
}
//-----------------------------------------------STEROWANIE-----------------------------------------------------
//-----------------------------------------------STEROWANIE-----------------------------------------------------
//-----------------------------------------------STEROWANIE-----------------------------------------------------
//-----------------------------------------------STEROWANIE-----------------------------------------------------
void ReadHumiditySensor(int sensorVal, int wetVal, int dryVal, int& storeValue){
  int percentageHumididy = map(sensorVal, dryVal, wetVal, 0, 100);
  storeValue = percentageHumididy;
}
void ReadHumidity(){
  digitalWrite(D1, HIGH);
  ReadHumiditySensor(analogRead(A0),wet,dry,soilMoisture);
  delay(200);
  digitalWrite(D1, LOW);
  ReadHumiditySensor(analogRead(A0),wet,dry,waterLevel);
}
void PumpWater(){
  digitalWrite(D2, LOW);
  delay(pumpingTime);
  digitalWrite(D2, HIGH);
}
