int dry = 710;
int wet = 340;
int soilMoisture = 10;
int waterLevel = 10;
String potName = "Noname";
void setup() {
  // put your setup code here, to run once:
Serial.begin(9600);
pinMode(D1, OUTPUT);
pinMode(D2, OUTPUT);
}

void ReadHumidity(int sensorVal, int wetVal, int dryVal, int& storeValue){
  int percentageHumididy = map(sensorVal, dryVal, wetVal, 0, 100);
  storeValue = percentageHumididy;
}
void ReadHumidity(int wet, int dry){
  digitalWrite(D1, HIGH);
  ReadHumidity(analogRead(A0),wet,dry,soilMoisture);
  delay(200);
  digitalWrite(D1, LOW);
  ReadHumidity(analogRead(A0),wet,dry,waterLevel);
}
void PrintStatus(String n,int wLevel,int sMoisture){
  Serial.println(n);
  String tmp = String(millis())+" "+String("waterLevel: ")+" "+String(wLevel)+ String("\%");
  Serial.println(tmp);
  tmp = String(millis())+" "+String("Soil Moisture: ")+" "+String(sMoisture)+ String("\%");
  Serial.println(tmp);
}
void PumpWater(){
  Serial.println("pompuje..");
  digitalWrite(D2, LOW);
  delay(4000);
  digitalWrite(D2, HIGH);
  Serial.println("...zakonczyl pompowac");
}

void loop() {
  // put your main code here, to run repeatedly:
  ReadHumidity(wet, dry);
  PrintStatus(potName,waterLevel,soilMoisture);
  if(soilMoisture<30){
    PumpWater();
  }
    
  delay(5000);


}
