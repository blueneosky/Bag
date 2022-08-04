#include <EEPROM.h>

const int gAMes = 0;
const int gA0 = 10;  // erreur à la con re-routage
const int gA1 = 11;  // erreur à la con re-routage
const int gA2 = 2;
const int gA3 = 3;
const int gAEn = 4;

const int gBMes = 1;
const int gB0 = 5;
const int gB1 = 6;
const int gB2 = 7;
const int gB3 = 8;
const int gBEn = 9;

const float trigerSensorVoltage = 4500; //mV

int nbMes = 10;

typedef struct EE_DATA eeData;
struct EE_DATA {
  char hardNumber;  // 1
  double a;         // 4
  double b;         // 4
  char empty[7];    // 7
                    //--
};                  //16

eeData sensorConf[32];

/********************** SETUP ********************************/

void setup()   {
  delay(1000);  // plusieur redémarrage arrive, on patiente un peut avant de faire qq chose
  
  pinMode(gA0, OUTPUT);
  pinMode(gA1, OUTPUT);
  pinMode(gA2, OUTPUT);
  pinMode(gA3, OUTPUT);
  pinMode(gAEn, OUTPUT);
  pinMode(gB0, OUTPUT);
  pinMode(gB1, OUTPUT);
  pinMode(gB2, OUTPUT);
  pinMode(gB3, OUTPUT);
  pinMode(gBEn, OUTPUT);
  
  Serial.begin(9600);          //  setup serial

  // lit la configuration des capteurs
  readEEConf();

  // selectionne la sonde 0, juste comme ca
  setActiveSensor(0);

  Serial.println("Start");
  delay(500);

  mainMenu();

}

/********************** LOOP ************************/
int getSensorValue(int number, double& v, double& r);

void loop()                     
{
  Serial.flush();
  
  double v, r;
  while (Serial.available()==0) {
    // pause, c'est le PC qui demande la série de mesure
    while(Serial.available()==0) delay(100);
    
    // scan all
    for (int i=0; i<32; i++) {
      if (!getSensorValue(i, v, r)) {
        Serial.print("Sensor ");
        if (i<=10) Serial.print(" ");
        Serial.print(i+1);
        Serial.print(" : ");
        Serial.println(rToCelcius(r));
      }
    }
    Serial.println("end");
    
    // vide le tampon d'entré
    Serial.flush();  // rien à foutre de ce qui à été écrit
  }
}

/**
 * Récupère la résistance pour un numéro de sonde
 * number : numéro de la sonde (coffret) (0 à 31)
 * v : retour de la tension
 * r : retour de la resistance
 * return : 0 si une sonde est connecté
 */
int getSensorValue(int number, double& v, double& r) {
  int hnb = sensorConf[number].hardNumber;
  // obtient le pin de mesure
  int pin = (hnb<16 ? gAMes : gBMes);
  // configuration pour la bonne sonde
  setActiveSensor(hnb);
  
  int old = -1;
  int mes = 0;
  while (old!=mes) {
    old = mes;
    // prise de mesure
    for (int i=0; i<nbMes; i++) {
      mes += analogRead(pin);
      delay(1);
    }
    mes /= nbMes;
  }
  // obtient la tension de lecture (mV)
  v = dmap(mes, 0, 1023, 0.0, 5000);
  
  // obtient la résistance de la sonde
  double a = sensorConf[number].a;
  double b = sensorConf[number].b;
  r = a*v+b;
  
  return v>=trigerSensorVoltage;  // capteur déconnecté => 1
}

int getSensorValue(int number, double& r) {
  double v;
  return getSensorValue(number, v, r);
}

double rToCelcius(double r) {
  return dmap(r, 1000, 1385, 0, 100);
}

void setActiveSensor(int number) {
  if (number<16) {  // groupe A
    int n = number;
    writeSensorConfig(n, gA0, gA1, gA2, gA3, gAEn);
    
  } else {          // groupe B
    int n = number - 16;
    writeSensorConfig(n, gB0, gB1, gB2, gB3, gBEn);
 }
}

void writeSensorConfig(int n, int p0, int p1, int p2, int p3, int pEn) {
    digitalWrite(gAEn, HIGH);  // désactive le groupe A
    digitalWrite(gBEn, HIGH);  // désactive le groupe B
    delay(1);
    
    digitalWrite(p0, (bitRead(n, 0) ? HIGH : LOW));
    digitalWrite(p1, (bitRead(n, 1) ? HIGH : LOW));
    digitalWrite(p2, (bitRead(n, 2) ? HIGH : LOW));
    digitalWrite(p3, (bitRead(n, 3) ? HIGH : LOW));
    digitalWrite(pEn, LOW);   // active le groupe
    delay(2);  // absorber le délais de stabilisation
}  

/*********************************************************/

double dmap(double x, double in_min, double in_max, double out_min, double out_max)
{
  return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}

/*********************************************************/

void readEEConf() {
  uint8_t* p = (uint8_t*)sensorConf;
  for (int i=0; i<512; i++) {
    *(p++) = EEPROM.read(i);
  }
}

void writeEEConf() {
  uint8_t* p = (uint8_t*)sensorConf;
  for (int i=0; i<512; i++) {
    EEPROM.write(i, *(p++));
  }
}

/***************************** MENU ************************/

void mainMenu() {
  int _start = 1;

  while (_start) {
    Serial.println("================================");
    Serial.println("Menu :");
    Serial.println("0 : Start");
    Serial.println("1 : Show R");
    Serial.println("2 : Quick Calibration");
    Serial.println("3 : Full Calibration (longer)");
    Serial.println("4 : Full quick show celcius");
    Serial.println(".....................");
    Serial.println("R : Reset Calibration");
    Serial.flush();  // petit clean
  
    int _showMenu = 1;
    while (_start && _showMenu) {
      // attent une entrée
      while(Serial.available()==0) delay(200);
      // traite l'entré
      switch (Serial.read()) {
        case '0':
          _start = 0;  // on démarre le vrai taff
          break;
        case '1':
          _showMenu = 0;
          menuShowR();
          break;
        case '2':
          _showMenu = 0;
          menuQuickCalibration();
          break;
        case '3':
          _showMenu = 0;
          menuFullCalibration();
          break;
        case '4':
          _showMenu = 0;
          menuQuickShowCelcius();
          break;
          
        case 'R':
        case 'r':
          _showMenu = 0;
          menuResetCalibration();
          break;
          
        default:
          break;
      }
    }
  }
}

void menuShowR() {
  Serial.println("================================");
  int number = -1;
  while (number<1 || number>32) {
    Serial.println("");
    Serial.println("Choose the sensor's number :");

    number = menuScanNumber();  // récupère le numéro

    Serial.print("Sensor ");
    Serial.println(number);
    
    // petite verif
    if (number<1 || number>32) {
      Serial.println("/!\\ incorect number!");
    }
  }
  
  Serial.println("Press any key to stop");
  delay(1000);
  
  Serial.flush();
  double value;
  int ret;
  number--;  // zero basé
  while(Serial.available()==0) {
    ret = getSensorValue(number, value);
    if (ret) {
      Serial.println("sensor not connected");
    } else {
      Serial.print(value);
      Serial.println(" ohms");
    }
  }
}

void menuQuickCalibration() {
  // avertissement
  Serial.println("================================");
  Serial.println("Please unplug all sensors/resitances");
  Serial.println("Press any key to continue...");
  Serial.flush();
  while (Serial.available()==0) delay(200);
  
  // config groupe 1 (noire)
  // attribution de tout les ports
  for (int i=0; i<8; i++) {
    sensorConf[i].hardNumber = 2*i;
    sensorConf[i+8].hardNumber = 2*i+1;
  }
  Serial.println("");
  Serial.println("===========");
  Serial.println("Black group");
  Serial.println("===========");
  menuSensorCalibration(0);      // calibration du capteur 1
  // on recopie la calibration pour ce groupe
  double a = sensorConf[0].a;
  double b = sensorConf[0].b;
  for (int i=1; i<16; i++) {
    sensorConf[i].a = a;
    sensorConf[i].b = b;
  }
  Serial.println();
  Serial.print("internal a=");
  Serial.println(a);
  Serial.print("internal b=");
  Serial.println(b);
  
  // config groupe 2 (rouge)
  // attribution de tout les ports
  for (int i=0; i<8; i++) {
    sensorConf[16+i].hardNumber = 16+2*i;
    sensorConf[16+i+8].hardNumber = 16+2*i+1;
  }
  Serial.println("");
  Serial.println("=========");
  Serial.println("Reg group");
  Serial.println("=========");
  menuSensorCalibration(16);      // calibration du capteur 17
  // on recopie la calibration pour ce groupe
  a = sensorConf[16].a;
  b = sensorConf[16].b;
  for (int i=17; i<32; i++) {
    sensorConf[i].a = a;
    sensorConf[i].b = b;
  }
  Serial.println();
  Serial.print("internal a=");
  Serial.println(a);
  Serial.print("internal b=");
  Serial.println(b);
  
  // sauvegarde
  Serial.println("Saving...");
  writeEEConf();
  
  Serial.println("");
  Serial.println("Quick calibration finish");
  delay(500);
}

void menuFullCalibration() {
  // avertissement
  Serial.println("================================");
  Serial.println("Please unplug all sensors/resitances");
  Serial.println("Press any key to continue...");
  Serial.flush();
  while (Serial.available()==0) delay(200);

  int _end = 1;
  while(_end) {
    Serial.println("");
    Serial.println("Select the sensor that you want to calibrate (0 to quit):");
    int n = menuScanNumber();
    if (n==0) {
      _end = 0;
    } else if (n<0 || n>32) {  // bad
      Serial.println("Bad sensor number, try again or 0 to quit");
    } else {
      Serial.print("Plug the calibration resistance in port ");
      Serial.println(n);
      int port = n-1;  // réadapte au fonctionnement interne
      delay(500);
      
      // on scan pour trouver le bon truc
      Serial.println("Scanning...");
      int hardPort = getActiveSensor();
      Serial.println("Scanning done");
      Serial.println("");
      Serial.println("");
      delay(500);
      sensorConf[port].hardNumber = hardPort;
      
      // on calibre
      menuSensorCalibration(port);
      
      // on enregistre
      Serial.println("Saving...");
      writeEEConf();
    }
  }
}

void menuSensorCalibration(int number) {
  double voltage[10];
  double resistance[10];
  int n = 0;
  double v;  // tension
  double r;  // resistance (calculé)
  int _fin = 1;
  
  Serial.print("Calibration of sensor ");
  Serial.println(number+1);
  Serial.println("Finalize with 10 measures or 'q' on waiting plug");
  Serial.flush();
  
  while (n<10 && _fin) {
    Serial.println("");
    Serial.print("Plug a resistance calibrator on ");
    Serial.println(number+1);
    
    // attend le branchement de la sonde ou q
    while (_fin && getSensorValue(number, v, r)) {
      if (Serial.available()>0 && Serial.read()=='q' && n>1) _fin = 0;
    }
    
    if (_fin) {  // realise la mesure
      // demande la valeur
      Serial.println("What is the resistance value?");
      resistance[n] = menuScanNumber();
      
      // fait la mesure final
      delay(500);
      getSensorValue(number, voltage[n], r);  // mesure retenu
      
      // attend le débranchement de la sonde
      Serial.println("Please unplug the sensor calibrator");
      while (!getSensorValue(number, v, r));
      
      n++;
    }
  }
  
  // on fait juste une régression linéaire sur cette ensemble de point
  double Xa = 0;
  double Xb = 0;
  double Xd = 0;
  double Ya = 0;
  double Yb = 0;
  double a,b;
  double _det;
  
  for (int i=0; i<n; i++) {
    v = voltage[i];
    r = resistance[i];
    Xa += v*v;
    Xb += v;
    Xd += 1;
    Ya += v*r;
    Yb += r;
  }
  _det = Xa*Xd - Xb*Xb;
  if (_det==0) {  // on est dans la merde, pb durant les mesures??!!
    // on prend des valeurs par defaut
    a = 0.5;
    b = -75.46;
  } else {
    a = (Xd*Ya - Xb*Yb)/_det;
    b = (Xa*Yb - Xb*Ya)/_det;
  }
  
  // enregistre ça
  sensorConf[number].a = a;
  sensorConf[number].b = b;
}

void menuResetCalibration() {
  Serial.println("================================");
  Serial.println("Being reset...");
  for (int i=0; i<16; i++) {
    sensorConf[i].a = 0.46;      // valeur cohérente, depuis une calibration
    sensorConf[i].b = -93.49;   // valeur cohérente, depuis une calibration
  }
  for (int i=16; i<32; i++) {
    sensorConf[i].a = 0.47;      // valeur cohérente, depuis une calibration
    sensorConf[i].b = -90.14;   // valeur cohérente, depuis une calibration
  }
  for (int i=0; i<8; i++) {
    sensorConf[i].hardNumber = 2*i;
    sensorConf[i+8].hardNumber = 2*i+1;
    sensorConf[16+i].hardNumber = 16+2*i;
    sensorConf[16+i+8].hardNumber = 16+2*i+1;
  }
  
  Serial.println("Saving...");
  writeEEConf();
  
  Serial.println("Reset done");
  delay(500);
}

int menuScanNumber() {
  // vide le tampon (entrée parasite suprimée)
  Serial.flush();
    
  // attend une entrée
  while (Serial.available()==0) delay(200);
  
  // traite le nombre reçu
  int number = 0;
  while(Serial.available()>0) {
   // très brut mais osef
    number = 10 * number + Serial.read() - '0';
  }
  
  return number;
}

// affiche de manière toute les température des capteurs branchés
void menuQuickShowCelcius() {
  Serial.println("================================");
  Serial.println("Press any key to stop quick show");
  delay(1000);
  Serial.flush();
  
  double r;
  while (Serial.available()==0) {
    // scan all
    for (int i=0; i<32 && Serial.available()==0; i++) {
      if (!getSensorValue(i, r)) {
        Serial.print("Sensor ");
        if (i<=10) Serial.print(" ");
        Serial.print(i+1);
        Serial.print(" : ");
        Serial.print(rToCelcius(r));
        Serial.println(" Celcius");
      }
    }
  }
}

int getActiveSensor() {
  int n = -1;
  double r;
  double v;
  while (n==-1) {
    for (int i=0; i<32 && n==-1; i++) {
      if (!getSensorValue(i, v, r)) {
        n = i;
      }
    }
  }
  
  return n;
}

