# Perceptron
Celem jest napisanie programu, który pobiera następujące argumenty: 
  a – stała uczenia, 
  train-set – nazwa pliku zawierającego zbiór treningowy w postaci CSV, 
  test-set – nazwa pliku zawierającego zbiór testowy w postaci CSV.

Należy zaimplementować perceptron, który wykorzystując podany train-set oraz stałą uczenia, nauczy się (reguła delta) rozpoznawać 2 klasy - w tym wypadku na przykładzie pliku z irysami.

Po nauce program ma podać procent poprawnie rozpoznanych kwiatów z test-setu oraz dodatkowo wyświetlić dokładność dla poszczególnych gatunków. 

Program ma dostarczać testowy interfejs (niekoniecznie graficzny), który umożliwia (zapętlone) podawanie przez użytkownika pojedynczych wektorów do klasyfikacji. 

Program ma działać poprawnie dla dowolnej liczby atrybutów oraz etykiet. 

Należy przetestować program na danych iris pomniejszonych o jeden gatunek (należy wybrać który) tak, aby zostało 100 kwiatów – po 50 z każdego pozostałego gatunku. 

Dane te należy podzielić na zbiór treningowy i testowy (35 do zbioru treningowego, a 15 do zbioru testowego z każdego gatunku).

Należy dostarczyć klasy:
  Perceptron (metody "compute", "learn", konstruktor(y)),
  Trainer (proces uczenia),
  UI (tekstowy lub graficzny, żeby zapewnić możliwość wygodnego testowania podczas zaliczenia, etc.).
