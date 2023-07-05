# Chorium

## Projektni tim

Ime i prezime | E-mail adresa (FOI) | Github korisničko ime
------------  | ------------------- | ---------------------
Karlo Vardijan | kvardijan20@student.foi.hr | kvardijan
Filip Šoštarić | fsostaric20@student.foi.hr | FSostaric20
Stanko Smrček | ssmrcek20@student.foi.hr | ssmrcek20

## Opis domene

Roditelji bi željeli program u kojem bi mogli zadavati i pratiti svoje poslove i poslove svoje dijece. Česta je pojava da roditelji djeci zadaju neke kućanske poslove koji bi se trebali riješiti. To su poslovi poput čišćenja sobe, pranja suđa, košenje travnjaka itd. S obzirom da djeca sve više vremena provode za računalima, roditelji bi željeli da djeca mogu i tamo vidjeti svoje obaveze. Djeci bi također bilo lakše pratiti što trebaju učiniti pomoću neke aplikacije kako nebi slučajno zaboravili. Ovo softwaresko rješenje bi rješavalo taj problem.

## Specifikacija projekta

Oznaka | Naziv | Kratki opis | Odgovorni član tima
------ | ----- | ----------- | -------------------
F01 | Login | Sustav će omogućiti prijavu u aplikaciju s dvije vrste korisnika. Dvije vrste korisnika su roditelj i dijete. Moći će se prijaviti sa korisnickim imenom i lozinkom ili prepoznavanjem lica. | Filip Šoštarić
F02 | Registracija | Sustav će omogućiti registraciju korisnika roditelj ili dijeteta u bazu podataka.| Stanko Smrček
F03 | Prikaz obiteljskog kalendara | Sustav će prikazati kućanske poslove kosrisnika u obliku mjesečnog kalendara. Korisnik će također moći dodati svoje obveze poput posla, škole, vrtića, treninga, i sl. u kalendar. | Karlo Vardijan
F04 | Dodavanje kućanskog posla | Korisnik roditelj će moći dodati kućanski posao u aplikaciju i dodjeliti ga nekom drugom korisniku ili sebi. Kućanski poslovi će biti kategorizirani (rad u vrtu, čišćenje, kuhanje i sl.) i imati će dobnu granicu. | Karlo Vardijan
F05 | Prikaz kućanskih poslova | Popis svih kućanskih poslova će biti prikazan na glavnoj formi. | Filip Šoštarić
F06 | Rješavanje kućanskih poslova | Korisnik će moći označiti kućanski posao da je gotov, ali ako je korisnik dijete onda roditelj mora prije odobriti da je kućanski posao stvarno gotov. | Karlo Vardijan
F07 | Filtriranje prikaza kućanskih poslova | Kod prikaza kućanskih poslova korisnici će moći filtrirati poslove po kategorijama, po korisniku, po stanju posla(završen i ne završen). | Filip Šoštarić
F08 | Podsjetnika za kućanski posao | Korisnik će moći postaviti vrijeme kada hoće primiti Windows obavijest za obavljanje nekog posla. Osim toga sustav će i svako jutro u 8 sati svakom ćlanu poslati mail sa popisom poslova  za taj dan, te još jedan mail na kraju dana sa popisom koji poslovi su obavljeni a koji nisu. | Stanko Smrček
F09 | Prikaz izvještaja | Sustav će na mjesečnoj razini prikazivati broj obavljanih poslova po korisniku na grafu. | Stanko Smrček

## Tehnologije i oprema

Tehnologije korištene u izradi ovog programskog rješenja su Microsoft WPF, .Net Framework, RDLC Report Designer, EmugCV, EntityFramework i UWP notifications. Kao sustav verzioniranja koristi se Git te GitHub. Za upravljanje bazom podataka smo koristili Microsoft SQL Server Management Studio.
