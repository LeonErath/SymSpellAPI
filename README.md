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

[nspell](https://www.npmjs.com/package/nspell)

[SymSpell](https://github.com/wolfgarbe/SymSpell)

[node-spellchecker](https://github.com/atom/node-spellchecker)


| algorithm         | precision      | recall         | accuracy       | time (in sec.) |
|-------------------|----------------|----------------|----------------|----------------|
| nspell            | 0.980792316927 % | 0.658870967742 % | 0.822983870968 % | 21.011 s         |
| SymSpell          | 0.818371607516 % | 0.632258064516 % | 0.745967741935 % | 2.73 s           |
| node-spellchecker | 0.988636363636 % | 0.701612903226 % | 0.846774193548 % | 9.165 s          |


Corpus: ABODAT.643 (2480 Words)