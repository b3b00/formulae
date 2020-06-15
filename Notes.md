# Notes



![notes 1](https://raw.githubusercontent.com/b3b00/formulae/master/specifications/IMG_20200614_152620.jpg)
![notes 2](https://raw.githubusercontent.com/b3b00/formulae/master/specifications/IMG_20200614_152532.jpg)


![note 1]({{site.baseurl}}//IMG_20200614_152620.jpg)
![note 1]({{site.baseurl}}//IMG_20200614_152532.jpg)


## but
Excel est un super outil de "dev"
Le but est de reproduire sa simplicité à base formules simples et réactives
Pour un sytème plus générique.

## description

le système est décrite par un ensemble de formules d'affectations de variables.

```
a = b + 1
b = <input>
```

Des variables spéciales (<input>) permettent de désigner des valeurs à saisir par l'utilisateur (UI) ou le système (SQL, network...)

## fonctionnement

A la compilation on génére un graphe de dépendances avec détection des cycles.

Lors de chaque modification de variable on recalcul toutes les dépendances y compris transitives.

## types de données

les types de données seront 
- entiers
- doubles / réels
- chaines de caractères
- booléen
- date

Les tableaux doivent pouvoir être gérés : quelle représentation dans les formules ?

les objet sont un concept plsu difficile à appréhender et ne seront pas gérés au moins dans un premier temps. 


## persistence

le système est une succession de transitions d'état. il est donc idéal pour utiliser de l'event sourcing (attention aux performances pour une durée de vie importante). 

Avantage : les transitions sont plus simple à persister qu'un ensemble de données complet.
