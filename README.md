# SymSpellAPI

Welcome to the SymSpell API! If you don't know what SymSpell is, please check out the awesomme [SymSpell-Spellchecker](https://github.com/wolfgarbe/SymSpell). This documentation should help you familiarise yourself with the resources available and how to consume them with HTTP requests. Read through the getting started section before you dive in. Most of your problems should be solved just by reading through it.

Feel free to suggest any changes or improvements. You can contact me via Github or my email address (leon-erath@hotmail.de).

## Get Started

The Web API implements [SymSpell](https://github.com/wolfgarbe/SymSpell) via the NuGet Package (Current Version SymSpell 6.3.0). 

```
git clone https://github.com/LeonErath/SymSpellAPI
```

You can deploy the code yourself or deploy in a docker container:

## Docker Container

To build and run the container:
```
docker build -t symspellapi .
docker run -it --rm -p 5000:80 symspellapi
```
The container should be then availabe on http://localhost:5000

# API

You can find the Documentation [here](https://documenter.getpostman.com/view/368567/SVYjU2du?version=latest). There you find a detailed description of all possible api calls as well as integration examples for all major programming languages.

# Performance

I currently evaluated 3 spellchecker in nodejs. Here are my results:

| algorithm         | precision      | recall         | accuracy       | time (in sec.) |
|-------------------|----------------|----------------|----------------|----------------|
| [nspell](https://www.npmjs.com/package/nspell)            | 98.0861 % | 66.129 % | 82.41935 % | 20.451 s         |
| [SymSpell](https://github.com/wolfgarbe/SymSpell)         | 98.3173 % | 65.96774 % | 82.419 % | 3.793 s           |
| [node-spellchecker](https://github.com/atom/node-spellchecker) | 98.86 % | 70.16129 % | 84.6774 % | 9.165 s          |


Corpus: [ABODAT.643 from Birkbeck spelling error corpus](http://ota.ox.ac.uk/headers/0643.xml) (2480 Words)

| algorithm         | precision      | recall         | accuracy       | time (in sec.) |
|-------------------|----------------|----------------|----------------|----------------|
| [nspell](https://www.npmjs.com/package/nspell)              | 97.577 % | 88.6 % | 93.2 % | 22.511 s         |
| [SymSpell](https://github.com/wolfgarbe/SymSpell)           | 97.99789 % | 93 % | 95.55 % | 1.999 s           |
|  [node-spellchecker](https://github.com/atom/node-spellchecker)| 98.333 % | 94.4 % | 96.4 % | 12.837 s          |


Corpus: [Wikipedia](https://en.wikipedia.org/wiki/Wikipedia:Lists_of_common_misspellings/For_machines) (the first 1000 Words)


| algorithm         | precision      | recall         | accuracy       | time (in sec.) |
|-------------------|----------------|----------------|----------------|----------------|
| [nspell](https://www.npmjs.com/package/nspell)            | 97.83599 % | 85.9 % | 92 % | 24.63 s         |
| [SymSpell](https://github.com/wolfgarbe/SymSpell)           | 96.868 % | 89.7 % | 93.4 % | 2.489 s           |
|  [node-spellchecker](https://github.com/atom/node-spellchecker)| 97.2073 % | 90.5 % | 93.95 % | 12.137 s          |


Corpus: [Wikipedia](https://en.wikipedia.org/wiki/Wikipedia:Lists_of_common_misspellings/For_machines) (the next 1000-2000 Words)