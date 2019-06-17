# wiimote

A C# library that implements a Nintendo Wii Remote (aka Wiimote) as a C# object.

This library was initially released as part of my university dissertation. It
is based on the work done by the Nintendo Wii homebrew community and the
original C# Wiimote implementation by
[Brian Peek](https://github.com/BrianPeek/WiimoteLib).

A test application is available for people to test and play with the library.
Note that it requires a device with bluetooth support in order to connect the
Wiimote. There are many tutorials online which detail the process of connecting
the Wiimote, I do not go into any detail on how to do that here, as there are
plenty of tutorials online which would be far superior to any instructions I
would provide.

Please note that this project has been ARCHIVED, and is not maintained. Anyone
wishing to interact with a Wiimote in C# is advised to use Brian Peek's
implementation which has received far more love and updates over the years,
versus this implementation which is barebones as required for my personal use.

## Dependencies

* [karlnicoll/hiddevices-cs](https://github.com/karlnicoll/hiddevices-cs)
* Windows.Forms for test app.

## Disclaimer

This project is not associated with Nintendo in any way, and is not in any way
endorsed or approved by Nintendo in any official capacity.

A sample WinForms application is provided to test the library.

## Authors

* Karl Nicoll

## License

This project is made available under the MIT license.
