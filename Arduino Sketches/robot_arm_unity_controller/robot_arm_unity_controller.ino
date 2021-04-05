#include <Servo.h>

// leds
#include <FastLED.h>

#define LED_PIN     9
#define NUM_LEDS    3
#define LED_TYPE    WS2812B
#define COLOR_ORDER GRB
CRGB leds[NUM_LEDS];
int blue = 0;

Servo myservos[6];  // create servo object to control a servo

int val[6];    // variable to read the value from the analog pin
int target_val[6];

int speed_fader_pin = 6;
int speed_fader_val = 0;
int analog_high_end = 1024;

void updateLEDs(CRGB col)
{
  for(int i = 0; i < NUM_LEDS; i++)
  {
    leds[i] = col;
  }
  FastLED.show();
}

void setup() {
  FastLED.addLeds<LED_TYPE, LED_PIN, COLOR_ORDER>(leds, NUM_LEDS).setCorrection( TypicalLEDStrip );
  FastLED.setBrightness(150);
  updateLEDs(CRGB(0,255,0));
  Serial.begin(9600);
  pinMode(LED_BUILTIN, OUTPUT);
  myservos[0].attach(6);
  //myservos[0].write(180);
  /*
  Serial.println("starting");
  int startpin = 3;
  for (int i = 0;  i < 6; i++) {
    myservos[i].attach(startpin+i);
    val[i] = target_val[i];
  }
  */
}

byte servoID = 0;
byte servoAngle = 0;

void loop() {
  if (Serial.available() > 0) {
    byte val = Serial.read();
    if (val <= 180 && val > 0) {
      servoAngle = val;
      myservos[0].write(servoAngle);
      updateLEDs(CRGB(servoAngle, 0, servoID * 50));
    } else if(val >= 250) {
      servoID = val - 250;
    }
  }
  //updateLEDs(CRGB(100, 100, blue));
}
