// <copyright file="PaypalReceiptTest.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Tests.Rules
{
    using System;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;
    using Acme.Automation.Rules;

    using Xunit;

    public class PaypalReceiptTest
    {
        private const string EmailTextBodyContabo = @"

19 nov 2019 05:38:51 CET Nº de transaction : 12345678901234567
https://www.paypal.com/myaccount/..

  [image: paypal]   19 nov 2019 05:38:51 CET
Nº de transaction : 12345678901234567
<https://www.paypal.com/myaccount/transaction/details/12345678901234567?utm_source=unp&utm_medium=email&utm_campaign=PPX001066&utm_unptid=db8c5127-0a88-11ea-9a7e-b875c00411a5&ppid=PPX001066&cnac=BE&rsta=fr_BE&cust=0D461220R7020333D&unptid=db8c5127-0a88-11ea-9a7e-b875c00411a5&calc=47fa0714522fd&unp_tpcid=email-receipt-xclick-payment&page=main:email:PPX001066:::&pgrp=main:email&e=cl&mchn=em&s=ci&mail=sys>


Bonjour PRISM,

Vous avez envoyé un paiement d'un montant de €99,99 EUR à CONTABO GmbH (
support@contabo.com)

Il est possible que la transaction n'apparaisse qu'au bout de quelques
minutes sur votre compte.
Marchand
CONTABO GmbH
support@contabo.com
+49 08921268372 Instructions au marchand
Vous n'avez pas saisi d'instructions.
Description Prix unitaire Qté Montant

€99,99 EUR 1 €99,99 EUR
* Sous-total : * €99,99 EUR
Total €99,99 EUR
Paiement €99,99 EUR
Le débit apparaîtra sur votre relevé de carte sous l'intitulé ""PAYPAL
*CONTABO""
Paiement envoyé à support@contabo.com
Vous rencontrez des problèmes avec cette transaction ?
Vous disposez de 180 jours à partir de la date de votre paiement PayPal
pour signaler un litige dans le Gestionnaire de litiges si la transaction
est éligible. En savoir plus sur la Protection des Achats
<https://www.paypal.com/be/webapps/mpp/buyer-protection?utm_source=unp&utm_medium=email&utm_campaign=PPX001066&utm_unptid=db8c5127-0a88-11ea-9a7e-b875c00411a5&ppid=PPX001066&cnac=BE&rsta=fr_BE&cust=0D461220R7020333D&unptid=db8c5127-0a88-11ea-9a7e-b875c00411a5&calc=47fa0714522fd&unp_tpcid=email-receipt-xclick-payment&page=main:email:PPX001066:::&pgrp=main:email&e=cl&mchn=em&s=ci&mail=sys>

 Des questions ? Accédez à l'Aide à l'adresse : www.paypal.com/be/help.

Veuillez ne pas répondre à cet email. Les messages reçus à cette adresse ne
sont pas lus et ne reçoivent donc aucune réponse. Pour obtenir de l'aide,
connectez-vous à votre compte PayPal et cliquez sur *Aide* en haut à droite
de chaque page PayPal.

Vous avez la possibilité de recevoir des emails en texte brut plutôt qu'au
format HTML. Pour changer vos préférences de Notification, connectez-vous à
votre compte, accédez à vos Préférences et cliquez sur *Paramètres du
compte*.



Copyright © 1999-2019 PayPal. Tous droits réservés.

PayPal (Europe) S.à r.l. et Cie, S.C.A. Société en Commandite par Actions
Siège social : 22–24 Boulevard Royal, L-2449 Luxembourg RCS Luxembourg B
118 349

PayPal PPX001066:1.1:47fa0714522fd";

        [Fact]
        public void ExecuteOk()
        {
            var message = new Message();
            message.Add("textBody", EmailTextBodyContabo);
            message.Add("subject", "Fwd: Reçu pour votre paiement à CONTABO GmbH");

            var job = new Job
            {
                Id = Guid.NewGuid().ToString(),
            };

            var rule = new PaypalReceipt
            {
                RuleConfiguration = new Rule
                {
                    Id = Guid.NewGuid().ToString()
                }
            };

            Assert.True(rule.IsMatch(job, message));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("this is a bad subject", EmailTextBodyContabo)]
        [InlineData("Reçu pour votre paiement à CONTABO GmbH", "This is a bad body")]
        public void ExecuteKoNotEmail(string subject, string textBody)
        {
            var message = new Message();
            message.Add("textBody", textBody);
            message.Add("subject", subject);

            var job = new Job
            {
                Id = Guid.NewGuid().ToString(),
            };

            var rule = new PaypalReceipt
            {
                RuleConfiguration = new Rule
                {
                    Id = Guid.NewGuid().ToString()
                }
            };

            Assert.False(rule.IsMatch(job, message));
        }
    }
}