# SocialBrothersAPICase
Om de API te gebruiken moet de repository gecloned worden en daarna via de gewenste IDE gerunt worden, er staan al 3 addressen in de database voor testsituaties maar mocht het
gewenst zijn om de database leeg te maken dan kan regel 24 in Startup.cs uitgecomment worden en dan wordt de tabel gereset.

Voor het aanmaken van adressen is er geprobeerd zoveel mogelijk rekening te houden met hoe adressen er wereldweid uitzien (er zijn  plekken waar geen postcodes gebruikt worden 
dus dat veld is nullable) en hierdoor is de validatie niet waterdicht, er is zoveel mogelijk rekening gehouden met de uiterste waardes maar gezien de grootte van de case en de 
beschikbare tijd heb ik hier geen topprioriteit aan gegeven.

GEBRUIK

Create:
Om een adres toe te voegen aan de database moeten Straat, Huisnummer, Toevoeging(optioneel), Postcode(optioneel, zie r.5-6 van deze README voor redenering), Plaats en Land worden meegegeven. Het is ook mogelijk om een id mee te geven maar dit komt door mijn onervaring met het maken van API's en ik weet niet hoe ik uit de parameter mogelijkheden moet halen zonder de modelvalidatie kwijt te raken dus nu is het een optioneel veld waar uiteindelijk niks mee gedaan wordt als het wel ingevuld is.

Read:
Om adressen op te halen kan de Get methode direct worden aangeroepen om alle adressen gesorteerd op id op te halen, ook kunnen er filters worden toegepast door voor het gewenste
veld een waarde mee te geven(Straat=a zorgt er voor dat er alleen adressen waarbij er een a in de straatnaam zit worden opgehaald) en er kan gesorteerd worden door in het orderby veld een sorteer string mee te geven. (bijv. Huisnummer desc)

Update:
Om te updaten moeten het id van het adres dat geupdate moet worden en de nieuwe waardes worden meegegeven, een lege waarde wordt gezien als geen verandering, omdat de velden
toevoeging en postcode ook leeg kunnen zijn is er voor gekozen om ze leeg te maken als er als waarde een "." wordt meegegeven, dit om ervoor te zorgen dat het leeglten van het
veld altijd dezelfde functie heeft maar het toch mogelijk is om nullable velden leeg te maken.

Delete:
Om een adres te verwijderen moet enkel het id worden meegegeven.

Afstanden:
Voor het berekenen van afstanden is het mogelijk om 2 adressen handmatig in te vullen (postcode wordt niet gebruikt voor de berekening dus dit is ook niet een van de invoervelden)
of de id's van 2 al ingevoerde adressen te gebruiken. De afstand wordt als double in kilometers teruggegeven.


Omdat dit de eerste keer is dat ik een API heb gemaakt ben ik er best trots op dat het binnen een week gelukt is om een werkende API neer te zetten maar ik ben ervan overtuigd dat er veel verbeteringen mogelijk zijn, vooral op het gebied van foutafhandeling ben ik tot de conclusie gekomen dat ik eigenlijk meer tijd nodig heb om het proces echt goed onder de knie te krijgen.
