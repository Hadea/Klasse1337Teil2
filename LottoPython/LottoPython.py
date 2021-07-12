import random
import datetime

# Lottoscheine vergleichen
def Compare(TicketA, TicketB):
   # trefferanzahl
   Trefferzahl = 0

   for DauerscheinElement in TicketA[:-2]:
      for ZiehungsElement in TicketB[:-2]:
         if DauerscheinElement == ZiehungsElement:
            Trefferzahl = Trefferzahl + 1
   if TicketA[5] == TicketB[5] or TicketA[5] == TicketB[6]:
      Trefferzahl = Trefferzahl + 1
   if TicketA[6] == TicketB[5] or TicketA[6] == TicketB[6]:
      Trefferzahl = Trefferzahl + 1
   return Trefferzahl


def Generate():
   Ticket = []
   while len(Ticket) < 5:
      randomNumber = random.randrange(1,51)
      if randomNumber not in Ticket:
         Ticket.append(randomNumber)
   Ticket.append(random.randrange(1,11))
   while True:
      randomNumber = random.randrange(1,11) 
      if randomNumber != Ticket[5]:
         Ticket.append(randomNumber)
         break
   return Ticket

# Lottoschein erstellen
Dauerschein = [9,12,17,22,44,1,8]

# Zweiten lottoschein erstellen
#Ziehung = [10,13,18,23,44,2,8]
Statistik = [0]*8

startTime = datetime.datetime.now()
for counter in range(0,1000000):
   Ziehung = Generate()
   hits = Compare(Dauerschein, Ziehung)
   Statistik[hits] +=1
# Anzahl der Treffer ausgeben
endTime = datetime.datetime.now()

print(endTime - startTime)
print(Statistik)






