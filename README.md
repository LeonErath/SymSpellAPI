# SymSpellAPI

Welcome to the SymSpell API! If you don't know what SymSpell is, please check out the awesomme [SymSpell-Spellchecker](https://github.com/wolfgarbe/SymSpell). This documentation should help you familiarise yourself with the resources available and how to consume them with HTTP requests. Read through the getting started section before you dive in. Most of your problems should be solved just by reading through it.

Feel free to suggest any changes or improvements. You can contact me via Github or my email address (leon-erath@hotmail.de).

## Get Started

The Web API implements [SymSpell](https://github.com/wolfgarbe/SymSpell) via the NuGet Package (Current Version SymSpell 6.3.0). 

```
git clone https://github.com/LeonErath/SymSpellAPI
```

You can find interactive examples here: https://runkit.com/leonerath/symspell-web-api

You can deploy the code yourself or deploy in a docker container:

## Docker Container

To build and run the container:
```
docker build -t symspellapi .
docker run -it --rm -p 5000:80 symspellapi
```
The container should be then availabe on http://localhost:5000

# API


### `POST` Lookup


**URL**

```sh
http://localhost:5000/lookup
```


**Parameters**

`document: string`

`distance: int`

`verbosity: int (0 Top,1 Closet,2 All)`


**Output**

```json
[
  {
    "term": "development",
    "distance": 2,
    "count": 286291411
  }
]
```


### `POST` LookupCompound


**URL**

```sh
http://localhost:5000/lookupcompound
```


**Parameters**

`document: string`

`distance: int`



**Output**

```json
[
  {
    "term": "where is the love he had dated for much of the past who couldn't read in sixth grade and inspired him",
    "distance": 9,
    "count": 300000
  }
]
```


### `POST` Word Stemming


**URL**

```sh
http://localhost:5000/wordstemming
```


**Parameters**

`document: string`

`distance: int`



**Output**

```json
{
  "item1": "the quick brown fox jumps over the lazy dog",
  "item2": "the quick brown fox jumps over the lazy dog",
  "item3": 8,
  "item4": -34.49116798191063
}
```
