IAE=0; Dt=0;  emax=0;opmax=0; n=0; pi=3.14; e2=0; Count=1;Tstic=0;
K=0.3631; Ti=0.0045; Kp=1; h=1; i =1; Dt=Dt+h; TotIAE=0; LM=0; LC =0;osc=0;
V_si=0; V_sq=0; IAE_lim = Ti/3.14; C=1; TD=0; TDhat=0; Top=0;opc = 0;  %Initialization
 
 OP = out.op.signals.values;  MV = out.mv.signals.values;
 Er = out.er.signals.values; E = table(Er,OP); tstop = 6007;
 
for t=1:tstop
    op = E.OP(i);
    Top = Top + op;
    opc = opc + 1;
    e = E.Er(i);
    i=i+1;
    Dt=Dt+h;
    if abs(e)>emax
       emax=abs(e);
    end
    
    if abs(e)>0.01
        osc = osc + 1;
    end
    
    if abs(e)>0.06
        LM = LM + 1;
        if abs(e)> 3
            LC = LC + 1;
        end
    end
    
    if abs(op)>opmax
       opmax=abs(op);
    end   
    if abs(e) > 0.45 
       e2=e2+abs(e);  
       n=n+1;
       n1 = n;
    else
        e=E.Er(i); 
        if e2>IAE_lim
           n = 0;
           IAE=e2;
           TotIAE=TotIAE+IAE;
           Tp=2*n1*h;
           a_si = (pi * IAE) / Tp;
           a_sq = (2 * IAE) / Tp;
           V_si = (abs(e) - a_si * sin((2 * pi / Tp) * h)) ^ 2;
           V_sq = (abs(e) - a_sq) ^ 2;
           Istic=(V_si - V_sq) / (V_sq + V_si);
           Count = Count+1;
           Tstic=Tstic+Istic;
        end
        if Dt>5*Ti
           Dy=IAE/Dt;
           if emax<2*Dy
              Dhat=(K/Ti*Dt-1/Kp)*Dy;
              C=C+1;
              TD=TD+Dhat;
           end
        end
    end   
end 
 Totstic=Tstic/Count;
 TDhat=TD/C;
 TOP = Top/opc;
 Tosc= (osc/i)*100;