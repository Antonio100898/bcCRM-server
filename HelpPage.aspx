<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelpPage.aspx.cs" Inherits="HelpPage" MasterPageFile="~/MainMaster.master" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .nav-link[data-toggle].collapsed:after {
            content: " ▾";
        }

        .nav-link[data-toggle]:not(.collapsed):after {
            content: " ▴";
        }
    </style>

    <%--    <div class="row text-center">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <a href="#" class="badge">פוס</a>
            <a href="#" class="badge">מדפסות</a>
            <a href="#" class="badge">בקאופיס</a>
            <a href="#" class="badge">דוח זד</a>
            <a href="#" class="badge">פריט</a>
            <a href="#" class="badge">באג</a>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-md-3 col-lg-3 pull-right" id="sidebar">
            <ul class="nav flex-column flex-nowrap overflow-hidden">
                <li class="nav-item">
                    <a class="nav-link" href="#divGeneralInfo"><span class="d-none d-sm-inline">מידע כללי</span></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#subPos"><span>פוס</span></a>
                    <div class="collapse" id="subPos" aria-expanded="false">
                        <ul class="nav">
                            <li class="nav-item"><a class="nav-link" href="#divPos"><span>מידע כללי</span></a></li>
                            <li class="nav-item"><a class="nav-link" href="#divPayments"><span>תשלומים</span></a></li>
                            <li class="nav-item"><a class="nav-link" href="#posPrinterSettings"><span>הגדרת מדפסות</span></a></li>
                        </ul>
                    </div>
                </li>
                <li class="nav-item">
                    <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#subBackoffice"><span>בקאופיס</span></a>
                    <div class="collapse" id="subBackoffice" aria-expanded="false">
                        <ul class="nav">
                            <li class="nav-item"><a class="nav-link" href="#divBackOffice"><span>מידע כללי</span></a></li>
                            <li class="nav-item"><a class="nav-link" href="#"><span>דוחות</span></a></li>
                        </ul>
                    </div>
                </li>


                <li class="nav-item">
                    <a class="nav-link" href="#divOuterComp"><span class="d-none d-sm-inline">חברות חיצוניות</span></a>
                </li>
            </ul>

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 20px">
                <p>יש לך רעיון או שאלה? תכתוב ונוסיף</p>
                <button class="btn btn-lg btn-primary col-xs-4 col-sm-4 col-md-4 col-lg-4" onclick="SendQuestion();">שלח</button>
                <input id="txtQuestion" class="input-lg col-xs-8 col-sm-8 col-md-8 col-lg-8" placeholder="הצעות או שאלות" />
            </div>
        </div>

        <%--style="max-height: 450px;overflow-y: scroll"--%>
        <div class="col-md-9 col-lg-9">
            <div id="divGeneralInfo" class="panel">
                <h2><a data-target="#sidebar" data-toggle="collapse"></a>מידע כללי</h2>
                <p>לביקום יש מספר קבצים</p>
                <p><b>POS</b> - התוכנה הראשית ליצירה וניהול הזמנות, קבלת תשלומים, מדפיסה חשבוניות ועוד</p>
                <p><b>BackOffice</b> - התוכנה לניהול דוחות כספים, מעקב משמרת עובדים, ויצירת דוח זד (סוף יום)</p>
                <p><b>Settings</b> - התוכנה לניהול תפריט והגדרות נוספות למערכת, עובדים, פריטים, מבצעים ועוד</p>
                <p><b>Data</b> - הקובץ שבו נשמרים הנתונים של יום העבודה</p>
                <p><b>DataBig</b> - הקובץ שאליו עוברים הנתונים בסוף יום עבודה, אחרי ביצוע זד</p>
                <p><b>Transfer</b> - התוכנה שאחראית להעביר את הנתונים מהData לDataBig</p>
            </div>

            <div id="divPos" class="panel">
                <h2><a data-target="#sidebar" data-toggle="collapse"></a>פוס</h2>
                <h6>שמות נוספים כמו posMisada, pos - bar only</h6>
                <p>לפוס יש 3 מסכים ראשיים</p>
                <p><b>מסך ראשי</b> - המסך שמציג את כל ההזמנות, מאפשר לפתוח הזמנה חדשה, להיכנס להזמנה, ולמסך התשלומים</p>
                <img width="300" height="250" src="Pics/helpStuff/popFrm_Tables.png" alt="מסך ראשי" />
                <h6>popFrm_Tables</h6>

                <p><b>מסך הזמנה</b> - מהמסך הזה אפשר לראות את פרטי ההזמנה, הפריטים שהזמינו, ולהוסיף פריטים</p>
                <img width="300" height="250" src="Pics/helpStuff/frm_OrdersRestaurant.png" alt="מסך הזמנה" />
                <h6>frm_OrdersRestaurant</h6>

                <p><b>מסך תשלומים</b> - מאפשר לשלם על ההזמנה</p>
                <img width="300" height="250" src="Pics/helpStuff/frm_NewPayment.png" alt="מסך תשלומים" />
                <h6>frm_NewPayment</h6>


                <h2 id="posPrinterSettings"><a data-target="#sidebar" data-toggle="collapse"></a>הגדרת מדפסות</h2>
                <img width="300" height="250" src="Pics/helpStuff/frm_printers.png" alt="מסך הגדרת מדפסות" />
                <h6>frm_printers</h6>


                <h2 id="divPayments"><a data-target="#sidebar" data-toggle="collapse"></a>תשלומים</h2>
                <table class="table table-hover text-right">
                    <thead>
                        <tr>
                            <th class="text-right">ID
                            </th>
                            <th class="text-right">תשלום
                            </th>
                            <th class="text-right">תיאור
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>1
                            </td>
                            <td>אשראי
                            </td>
                            <td>אשראי של חברת אשראית
                            </td>
                        </tr>
                        <tr>
                            <td>2
                            </td>
                            <td>מזומן
                            </td>
                            <td>שטרות כסף, שיטה שנכחדה עד מלחמת הביטקוין ב2028
                            </td>
                        </tr>
                        <tr>
                            <td>3
                            </td>
                            <td>שיק או צ'יק
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>4
                            </td>
                            <td>קופונים - שוברים
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>5
                            </td>
                            <td>הקפה
                            </td>
                            <td>מאפשר לשמור הקפה על לקוח, ושיחזיר את הכסף בזמן מאוחר יותר
                            </td>
                        </tr>
                        <tr>
                            <td>6
                            </td>
                            <td>אשראי חיצוני
                            </td>
                            <td>תשלום טיפש
                            </td>
                        </tr>
                        <tr>
                            <td>7
                            </td>
                            <td>ValueCard
                            </td>
                            <td>חברת מועדון לקוחות
                            </td>
                        </tr>
                        <tr>
                            <td>8
                            </td>
                            <td>תשלום טיפש 8
                            </td>
                            <td>תשלום סתמי, אפשר לקרוא לו איך שהלקוח רוצה
                            </td>
                        </tr>
                        <tr>
                            <td>9
                            </td>
                            <td>תשלום טיפש 9
                            </td>
                            <td>תשלום סתמי, אפשר לקרוא לו איך שהלקוח רוצה
                            </td>
                        </tr>
                        <tr>
                            <td>10
                            </td>
                            <td>סודקסו - sodexo - סיבוס לשעבר
                            </td>
                            <td>חברת מועדון לקוחות
                            </td>
                        </tr>
                        <tr>
                            <td>11
                            </td>
                            <td>MultiPass - מולטיפס
                            </td>
                            <td>חברת מועדון לקוחות
                            </td>
                        </tr>
                        <tr>
                            <td>12
                            </td>
                            <td>10Bis - תן ביס
                            </td>
                            <td>חברת מועדון לקוחות
                            </td>
                        </tr>
                        <tr>
                            <td>13
                            </td>
                            <td>הנחה
                            </td>
                            <td>תשלום בשימוש הפנימי של הפוס
                            </td>
                        </tr>
                        <tr>
                            <td>14
                            </td>
                            <td>פיצוי תשלום ללקוח
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>15
                            </td>
                            <td>Zooz
                            </td>
                            <td>תשלום שכבר לא פעיל
                            </td>
                        </tr>
                        <tr>
                            <td>16
                            </td>
                            <td>הנחה
                            </td>
                            <td>בשימוש פנימי של הפוס
                            </td>
                        </tr>
                        <tr>
                            <td>17
                            </td>
                            <td>פחת
                            </td>
                            <td>תשלום לפעולת פחת
                            </td>
                        </tr>
                        <tr>
                            <td>18
                            </td>
                            <td>BeeClub
                            </td>
                            <td>חברת מועדון לקוחות של ביקום
                            </td>
                        </tr>
                        <tr>
                            <td>19
                            </td>
                            <td>MyCheck
                            </td>
                            <td>החברה כבר לא פעילה
                            </td>
                        </tr>
                        <tr>
                            <td>20
                            </td>
                            <td>התחייבות לתשלום
                            </td>
                            <td>כחלק ממנגנון החזר תשלום של לקוח הקפה
                            </td>
                        </tr>
                        <tr>
                            <td>21
                            </td>
                            <td>IService
                            </td>
                            <td>לא פעילים
                            </td>
                        </tr>
                        <tr>
                            <td>23
                            </td>
                            <td>PayPhone
                            </td>
                            <td>חברת מועדון לקוחות
                            </td>
                        </tr>
                        <tr>
                            <td>24
                            </td>
                            <td>משלוחה
                            </td>
                            <td>תשלום מצד חברת משלוחה</td>
                        </tr>
                         <tr>
                            <td>25
                            </td>
                            <td>החזר payPhone
                            </td>
                            <td>חלק מהמנגנון של payPhone
                            </td>
                        </tr>
                         <tr>
                            <td>26
                            </td>
                            <td>MicroDeal
                            </td>
                            <td>לא פעילים
                            </td>
                        </tr>
                         <tr>
                            <td>27
                            </td>
                            <td>Goodi
                            </td>
                            <td>חברת מועדון לקוחות
                            </td>
                        </tr>
                         <tr>
                            <td>28
                            </td>
                            <td>Rewardy
                            </td>
                            <td>לא פעילים
                            </td>
                        </tr>
                         <tr>
                            <td>29
                            </td>
                            <td>Optima - אופטימה
                            </td>
                            <td>חברת תשלום במלונות, חיוב על החדר
                            </td>
                        </tr>
                         <tr>
                            <td>30
                            </td>
                            <td>FoodBook  - ספר האוכל
                            </td>
                            <td>
                            </td>
                        </tr>
                         <tr>
                            <td>31
                            </td>
                            <td>BitGo
                            </td>
                            <td>תשלום ביטקון
                            </td>
                        </tr>
                         <tr>
                            <td>32
                            </td>
                            <td>משלוחה
                            </td>
                            <td>תשלום של משלוחה
                            </td>
                        </tr>
                         <tr>
                            <td>33
                            </td>
                            <td>Keeprz
                            </td>
                            <td>חברת מועדון לקוחות
                            </td>
                        </tr>
                        <tr>
                            <td>34
                            </td>
                            <td>Beengo
                            </td>
                            <td>
                            </td>
                        </tr> 
                        <tr>
                            <td>35
                            </td>
                            <td>תשלום טיפש 35
                            </td>
                            <td>תשלום לשימוש המסעדה
                            </td>
                        </tr>
                        <tr>
                            <td>36
                            </td>
                            <td>תשלום טיפש 36
                            </td>
                            <td>תשלום לשימוש המסעדה
                            </td>
                        </tr>
                        <tr>
                            <td>37
                            </td>
                            <td>תשלום טיפש 37
                            </td>
                            <td>תשלום לשימוש המסעדה
                            </td>
                        </tr>
                        <tr>
                            <td>38
                            </td>
                            <td>תשלום טיפש 38
                            </td>
                            <td>תשלום לשימוש המסעדה
                            </td>
                        </tr>
                        <tr>
                            <td>39
                            </td>
                            <td>REST
                            </td>
                            <td>תשלום לשימוש Rest בעת משלוח
                            </td>
                        </tr>
                        <tr>
                            <td>40
                            </td>
                            <td>תו הזהב
                            </td>
                            <td>קופונים
                            </td>
                        </tr>
                        <tr>
                            <td>41
                            </td>
                            <td>miniHotel
                            </td>
                            <td>תשלום לתוכנת מיני הוטל
                            </td>
                        </tr>
                        <tr>
                            <td>42
                            </td>
                            <td>CreditX
                            </td>
                            <td>חברת אשראי
                            </td>
                        </tr>
                        <tr>
                            <td>43
                            </td>
                            <td>paidIt
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>44
                            </td>
                            <td>simplyClub
                            </td>
                            <td>חברת מועדון לקוחות
                            </td>
                        </tr>
                        <tr>
                            <td>45
                            </td>
                            <td>mgweb - משלוחים
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>46
                            </td>
                            <td>bazelet - משלוחים
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>47
                            </td>
                            <td>amazing - משלוחים
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>48
                            </td>
                            <td>Fortress
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>49
                            </td>
                            <td>Knopu
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>50
                            </td>
                            <td>Emv
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>51
                            </td>
                            <td>Zooz - Rest
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>52
                            </td>
                            <td>תמורה
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>53
                            </td>
                            <td>Modularity
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>54
                            </td>
                            <td>BeeClub
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>55
                            </td>
                            <td>גודי חיצוני
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>56
                            </td>
                            <td>תשלום חיצוני 56
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>57
                            </td>
                            <td>BITE
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>58
                            </td>
                            <td>סנג'ר
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>59
                            </td>
                            <td>עמיהוד- קפה נחת
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>60
                            </td>
                            <td>Urbanana
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>61
                            </td>
                            <td>inmanage
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>62
                            </td>
                            <td>BeecommClub</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>63
                            </td>
                            <td>Pax</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>64
                            </td>
                            <td>Tipper</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>65
                            </td>
                            <td>parallax</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>66
                            </td>
                            <td>wolt</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>67
                            </td>
                            <td>משלוחה 2</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>68
                            </td>
                            <td>Pay</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>69
                            </td>
                            <td>wisePay</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>70
                            </td>
                            <td>Brinks</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>71
                            </td>
                            <td>urbanana App</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>72</td>
                            <td>hapina</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>73
                            </td>
                            <td>jai</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>74
                            </td>
                            <td>ilansBoxes</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>75
                            </td>
                            <td>interService</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>76
                            </td>
                            <td>שובר מקומי</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>77
                            </td>
                            <td>Nayax Emv</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>78
                            </td>
                            <td>Bit</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>79
                            </td>
                            <td>רמי לוי</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>80
                            </td>
                            <td>letGroup</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>81
                            </td>
                            <td>GetFood</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>82
                            </td>
                            <td>&nbsp;</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>83</td>
                            <td>סיבוס תו הזהב</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>84
                            </td>
                            <td>DejaVoo</td>
                            <td>
                                חברת אשראי</td>
                        </tr>
                    </tbody>
                </table>

            </div>

            <div id="divBackOffice" class="panel">
                <h2><a data-target="#sidebar" data-toggle="collapse"></a>בקאופיס</h2>
                <h6>לניהול דוחות כספים</h6>
                <p>בבקאופיס ניתן להפיק דוחות, ולעשות זד</p>
                <p>מסך דוחות שממנו ניתן להפיק דוחות, ליצא אקסל, ולהפיק דוח Z ו X</p>
                <img width="300" height="250" src="Pics/helpStuff/backOffice_Proforma.png" alt="מסך דוחות" />
                <h6>Proforma</h6>
            </div>

            <div id="divOuterComp" class="panel">
                <h2><a data-target="#sidebar" data-toggle="collapse"></a>חברות חיצוניות</h2>
                <row>
                    <p><b>רסטורן</b> ניהול תוכנת מארחת, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>hype</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>paidIt</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>beengo</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>Mini Hotel</b> חברה במלונות, חיוב על החדר, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>Optima</b> תשלום במלונות, חיוב על החדר, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>Cali</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>ShipIT</b> חברה לניהול שליחים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>DelivApp</b> חברה לניהול שליחים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>KnuPo</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>Zap Rest</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>Restigo - רסטיגו</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>DragonTail</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>Cleap</b> חברה לניהול כספים, הגדרות בסייען</p>
                </row>
                <row>
                    <p><b>תן ביס</b> חברת משלוחים, ותשלום בפוס, הגדרוות בפוס ומרכז הזמנות</p>
                </row>
                <row>
                    <p><b>סיבוס</b> חברת משלוחים, ותשלום בפוס, הגדרוות בפוס ומרכז הזמנות</p>
                </row>
            </div>

            <%--        <div class="panel">
            <h2><a data-target="#sidebar" data-toggle="collapse"></a>כותרת</h2>
            <h6>תיאור נוסף קטן</h6>
            <p>תוכן ההודעה</p>
        </div>--%>
        </div>
    </div>

    <script type="text/javascript" class="init">

        function SendPostLocal(commandName, dataInfo, onsuccess, onError) {
            //showLoader();

            //console.log("Start SendPost");
            $.ajax({
                type: "POST",
                url: "HelpPage.aspx/" + commandName,
                data: dataInfo,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onsuccess,
                error: onError
            });
        }

        function SendQuestion() {
            var q = $("#txtQuestion").val();
            SendPostLocal("AddQuestion", '{q:"' + q + '"}', function (response) {

                $("#txtQuestion").val("");
                return true;

            },
                function (req, response) { console.log(req.d); });
        }

    </script>


</asp:Content>
