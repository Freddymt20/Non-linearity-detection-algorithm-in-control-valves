

'Date Average of 1H

Sub Stiction_Loop1H()

Dim R As Range
Dim R2 As Variant
Dim Row, Row2, h, Tp, n, Ei, ni, Ti, IAEL, a_si, a_sq As Integer
Dim Td, c As Integer
Dim Istic, V_si, V_sq As Long
Dim pi As Double
Dim E As Single

' Inicializadas

n = 0: Ti = Range("Row#"): pi = 3.14159265
K = Range("Row#"): Kp = Range("Row#"): opc = 0
IAEL = Ti / pi: T = 0: h = 60
LC = Range("Row#"): LRE = Range("Row#"): osc = Range("Row#")
LCrt = 0: Lr = 0: Tosc = 0


      For Each OP In Sheets("Sheet#").Range(Range("Row#"), Range("Row#").End(xlDown)).Cells
          OPT = OPT + OP 
          opc = opc + 1 
          T = T + 1
          If OP > opmax Then
             opmax = OP
          End If
      Next OP

      Range("Row#") = Round(opmax, 2): Range("Row#") = Round(OPT / opc, 2)

      For Each R In Sheets("Sheet#").Range(Range("Row#"), Range("Row#").End(xlDown)).Cells

          If R.Value > emax Then
             emax = R
          End If
          ET = ET + R

          If R.Value > osc Then
             Tosc = Tosc + 1

             If R.Value > LRE Then
                Lr = Lr + 1

                If R.Value > LC Then
                   LCrt = LCrt + 1
                End If

             End If

          End If
          
          If R.Value > 0 Then
          R2 = R2 + Round(R, 3)
          n = n + 1
          Else
              Dt = Dt + h

              If R = 0 Then
                 Ei = Round(R2, 3)
                 n1 = n
                 n = 0: R2 = 0
              End If
                          
              If Ei > IAEL Then
                 IAE = Ei
                 Tp = 2 * n1 * h
                 a_si = (pi * IAE) / Tp: a_sq = (2 * IAE) / Tp
                 V_si = (R4 - a_si * Sin((2 * pi / Tp) * h)) ^ 2
                 V_sq = (R4 - a_sq) ^ 2
                 Istic = (V_si - V_sq) / (V_sq + V_si)
                 TStic = TStic + Istic
                 i = i + 1
                 TIAE = TIAE + IAE
                 Range("Row#") = Round((TStic / i), 2)
              End If
              
              ' Backlash
              If Dt > 5 * Ti Then
                 Dy = TIAE / Dt
                 If emax < (2 * Dy) Then
                    d = ((K / Ti) * Dt - (1 / Kp)) * Dy

                    If d < 10 Then
                       Td = Td + d
                       c = c + 1
                    Else
                        Range("Row#") = "Na"
                        Sheets("REPORTS").Range("Row#") = "Na"
                    End If

                 End If

              End If

          End If
          R4 = R

      Next R
      
      Range("Row#") = Round(Td, 2)
      Med = ET / T
      Range("Row#") = Med
      Sq = (T - Med) ^ 2
      Prtosc = (Tosc / T) * 100 ' Range("Row#")") = t "Numero de muestras"
      Devst = Sqr(Sq / (T - 1))
      Sheets("REPORTS").Range("Row#") = Round(Td, 2)
      Range("Row#") = Devst
      Range("Row#") = emax
      Sheets("REPORTS").Range("Row#") = emax
      Range("Row#") = Prtosc
      Range("Row#") = Lr
      Range("Row#") = LCrt
      Sheets("REPORTS").Range("Row#") = Lr
      Range("Row#") = LCrt
      Range("Row#") = TIAE
      Sheets("REPORTS").Range("Row#") = TIAE

      ' Indicator summary interface cleanup 
      If (TStic = "") Then Range("Row#") = ""
      If (TStic = "") Then Sheets("REPORTS").Range("Row#") = ""
      If (TIAE = "") Then Range("Row#") = ""
      If (TIAE = "") Then Sheets("REPORTS").Range("Row#") = ""
      If (Td = "") Then Range("Row#") = ""
      If (Td = "") Then Sheets("REPORTS").Range("Row#") = ""
      If (emax = "") Then Range("Row#") = ""
      If (emax = "") Then Sheets("REPORTS").Range("Row#") = ""
      If (Prtosc = "") Then Range("Row#") = ""
      If (Prtosc = "") Then Sheets("REPORTS").Range("Row#") = ""
      If (LCrt = "") Then Range("Row#") = ""
      If (LCrt = "") Then Sheets("REPORTS").Range("Row#") = ""
      If (Lr = "") Then Range("Row#") = ""
      If (Lr = "") Then Sheets("REPORTS").Range("Row#") = ""
      
End Sub
