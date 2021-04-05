#include <Servo.h>

/*
// leds for testing and debugging purposes
#include <FastLED.h>

#define LED_PIN     9
#define NUM_LEDS    3
#define LED_TYPE    WS2812B
#define COLOR_ORDER GRB
CRGB leds[NUM_LEDS];
int blue = 0;

void updateLEDs(CRGB col)
{
  for(int i = 0; i < NUM_LEDS; i++)
  {
    leds[i] = col;
  }
  FastLED.show();
}
*/

#define NUM_AXES    6
Servo myservos[NUM_AXES];

int val[NUM_AXES];
int target_val[NUM_AXES];

int analog_high_end = 1024;

void setup() {
  //FastLED.addLeds<LED_TYPE, LED_PIN, COLOR_ORDER>(leds, NUM_LEDS).setCorrection( TypicalLEDStrip );
  //FastLED.setBrightness(150);
  //updateLEDs(CRGB(0,255,0));
  
  Serial.begin(9600);
  Serial.println("starting");
  int startpin = 3;
  for (int i = 0;  i < NUM_AXES; i++) {
    myservos[i].attach(startpin+i);
    val[i] = target_val[i] = 90;
  }
  
}

// id of the servo which is updated next
byte servoID = 0;

void loop() {
  // fetch and process serial input
  while (Serial.available() > 0) {
    byte input_value = Serial.read();
    if (input_value <= 180 && input_value > 0) {
      target_val[servoID] = input_value;
    } else if(input_value >= 250) {
      servoID = input_value - 250;
    }
  }

  // update servos
  for (int i = 0; i < NUM_AXES; i++) {
    if (target_val[i] > val[i]) {
      val[i]++;
    } else if (target_val[i] < val[i]) {
      val[i]--;
    }
    
    myservos[i].write(val[i]);
  }

  // delay controls speed of the robot's motion
  delay(25);
}
