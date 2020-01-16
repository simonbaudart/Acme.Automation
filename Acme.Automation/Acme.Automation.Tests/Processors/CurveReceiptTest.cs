// <copyright file="CurveReceiptTest.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Tests.Processors
{
    using System;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;
    using Acme.Automation.Core.Models;
    using Acme.Automation.Processors;

    using Xunit;

    public class CurveReceiptTest
    {
                private const string EmailTextHtmlGreco = @"<!doctype html>
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
<head>
  <!--[if gte mso 15]>
  <xml>
      <o:OfficeDocumentSettings>
          <o:AllowPNG/>
          <o:PixelsPerInch>96</o:PixelsPerInch>
      </o:OfficeDocumentSettings>
  </xml>
  <![endif]-->
  <meta charset=""UTF-8"">
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"">

  <style type=""text/css"">
    p{
      margin:10px 0;
      padding:0;
    }

    table{
      border-collapse:collapse;
    }

    h1,h2,h3,h4,h5,h6{
      display:block;
      margin:0;
      padding:0;
    }

    img {
      border: 0;
      height: auto;
      line-height: 100%;
      outline: none;
      text-decoration: none;
    }
    
    body,#bodyTable,#bodyCell{
      height:100%;
      margin:0;
      padding:0;
      width:100%;
      font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
    }

    #outlook a{
      padding:0;
    }

    img{
      -ms-interpolation-mode:bicubic;
    }

    table{
      mso-table-lspace:0pt;
      mso-table-rspace:0pt;
    }

    .ReadMsgBody{
      width:100%;
    }

    .ExternalClass{
      width:100%;
    }

    p,a,li,td,blockquote{
      mso-line-height-rule:exactly;
    }

    a[href^=tel],a[href^=sms]{
      color:inherit;
      cursor:default;
      text-decoration:none;
    }

    p,a,li,td,body,table,blockquote{
      -ms-text-size-adjust:100%;
      -webkit-text-size-adjust:100%;
      font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
      font-size: 15px;
    }

    .ExternalClass,.ExternalClass p,.ExternalClass td,.ExternalClass div,.ExternalClass span,.ExternalClass font{
      line-height:100%;
    }

    a[x-apple-data-detectors]{
      color:inherit !important;
      text-decoration:none !important;
      font-size:inherit !important;
      font-family:inherit !important;
      font-weight:inherit !important;
      line-height:inherit !important;
    }

    #bodyCell{
        padding:20px;
    }

    #templateContainer {
      width: 600px;
    }

    body,#bodyTable{
      background-color:#FAFAFA;
    }

    #bodyCell{
      border-top:0;
    }

    .templateContainer{
      border:0;
    }

    h1 {
      display: block;
      font-size: 20px;
      font-style: normal;
      font-weight: 700;
      line-height: 1;
      letter-spacing: normal;
      margin-top: 0;
      margin-right: 0;
      margin-bottom: 0;
      margin-left: 0;
      text-align: center;
      text-transform: uppercase;
    }

    h2 {
      font-size: 18px;
    }

    a:link,
    a:visited,
    a.yshortcuts {
      color: #000000;
      font-weight: normal;
      text-decoration: underline;
    }

    /* container styles */
    #ReceiptContainer,
    .NoteContainer {
      border: 1px solid #CCCCCC;
      padding-top: 32px;
      padding-left: 32px;
      padding-right: 32px;
      padding-bottom: 32px;
      background-color: #FFFFFF;
    }

    #ShareContainer {
      padding-top: 96px;
      padding-left: 0;
      padding-right: 0;
      padding-bottom: 96px;
      background-image: url('https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/bank-notes.png');
      background-repeat:no-repeat; 
      background-size:160px;
      background-position: center right;
    }

    #NoteContainer {
      padding-top: 96px;
      padding-left: 0;
      padding-right: 0;
      padding-bottom: 96px;
    }

    .ShareHeader {
      position: relative;
    }

    /* utility classes */
    .u-border__top {
      border-top: 1px solid #ccc;
    }

    .u-padding__topBottom {
      padding-top: 32px;
      padding-left: 0;
      padding-right: 0;
      padding-bottom: 32px;
    }

    .u-padding__topBottom--half {
      padding-top: 16px;
      padding-left: 0;
      padding-right: 0;
      padding-bottom: 16px;
    }

    .u-padding__topBottom--double {
      padding-top: 64px;
      padding-left: 0;
      padding-right: 0;
      padding-bottom: 64px;
    }

    .u-padding__top {
      padding-top: 32px;
      padding-left: 0;
      padding-right: 0;
      padding-bottom: 0;
    }

    .u-padding__top--half {
      padding-top: 16px;
      padding-left: 0;
      padding-right: 0;
      padding-bottom: 0;
    }

    .u-padding__bottom {
      padding-left: 0;
      padding-right: 0;
      padding-bottom: 32px;
    }

    .u-greySmaller {
      color: #737373;
      font-size: 13px !important;
    }

    .u-flowText {
      line-height: 1.4;
    }

    .u-imgMaxWidth {
      max-width: 100%;
    }

    .u-backgroundWhite {
      background: #ffffff;
    }

    .u-button {
      padding: 15px !important;
      text-align: center;
      text-decoration: none !important;
      color: #FFFFFF !important;
      display: inline-block;
      background: #57CBF7;
      border-radius: 2px;
    }

    .u-bold {
      font-weight: bold;
    }

    @media only screen and (max-width: 440px) {
      body,
      table,
      td,
      p,
      a,
      li,
      blockquote {
        -webkit-text-size-adjust: none !important;
        font-size: 14px !important;
      }

      h1 {
        font-size: 17px;
      }

      #ReceiptContainer,
      .NoteContainer {
        padding-top: 20px !important;
        padding-left: 20px !important;
        padding-right: 20px !important;
        padding-bottom: 20px !important;
      }

      #ShareContainer {
        padding-top: 72px !important;
        padding-bottom: 72px !important;
        background-size: 120px !important;
      }

      .u-padding__topBottom {
        padding-top: 20px !important;
        padding-bottom: 20px !important;
      }

      .u-padding__topBottom--half {
        padding-top: 12px !important;
        padding-bottom: 12px !important;
      }

      .u-padding__topBottom--double {
        padding-top: 40px !important;
        padding-left: 0 !important;
        padding-right: 0 !important;
        padding-bottom: 40px !important;
      }

      .u-padding__top {
        padding-top: 20px !important;
      }

      .u-padding__top--half {
        padding-top: 12px !important;
      }

      .u-padding__bottom {
        padding-bottom: 20px !important;
      }

      .u-greySmaller {
        font-size: 12px !important;
      }
    }

    @media only screen and (max-width: 632px) {
      body,
      table,
      td,
      p,
      a,
      li,
      blockquote {
        -webkit-text-size-adjust: none !important;
        font-size: 14px !important;
      }

      body {
        width: 100% !important;
        min-width: 100% !important;
      }

      #bodyCell {
        padding: 16px !important;
      }

      #templateContainer {
        max-width: 600px !important;
        width: 100% !important;
      }
      .HalfColumn {
        display: block !important;
        width: 100% !important;
        padding-top: 16px !important;
        text-align: center !important;
      }
    }
  </style>
</head>
<body leftmargin=""0"" marginwidth=""0"" topmargin=""0"" marginheight=""0"" offset=""0"" style=""height: 100%;margin: 0;padding: 0;width: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-size: 15px;background-color: #FAFAFA;"">
  <center>
    <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" height=""100%"" width=""100%"" id=""bodyTable"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;height: 100%;margin: 0;padding: 0;width: 100%;background-color: #FAFAFA;"">
      <tr>
        <td align=""center"" valign=""top"" id=""bodyCell"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;height: 100%;margin: 0;padding: 20px;width: 100%;border-top: 0;"">
          <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" id=""templateContainer"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;width: 600px;"">
            <tr>
              <td align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                <table border=""0"" cellpadding=""40"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                  <tr>
                    <td align=""center"" valign=""top"" class=""headerContent"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                      <a href=""https://www.curve.app/?utm_source=emailreceipt&utm_medium=email&utm_campaign=direct"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">
                        <img src=""https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/curve-logo.png"" style=""max-width: 160px;border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"" id=""headerImage"">
                      </a>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td align=""center"" valign=""top"" id=""ReceiptContainer"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;border: 1px solid #CCCCCC;padding-top: 32px;padding-left: 32px;padding-right: 32px;padding-bottom: 32px;background-color: #FFFFFF;"">
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                  <tbody>
                    <tr>
                      <td class=""u-padding__bottom"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;padding-left: 0;padding-right: 0;padding-bottom: 32px;"">
                        <h1 style=""display: block;margin: 0;padding: 0;font-size: 20px;font-style: normal;font-weight: 700;line-height: 1;letter-spacing: normal;margin-top: 0;margin-right: 0;margin-bottom: 0;margin-left: 0;text-align: center;text-transform: uppercase;"">Email Receipt</h1>
                      </td>
                    </tr>
                    <tr>
                      <td class=""u-padding__topBottom u-border__top"" align=""left"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;border-top: 1px solid #ccc;padding-top: 32px;padding-left: 0;padding-right: 0;padding-bottom: 32px;"">
                        Hello Simon,
                        <br>
                        <br>
                        <br>
                        You made a purchase at:
                      </td>
                    </tr>
                    <tr>
                      <td class=""u-padding__topBottom--half u-border__top"" align=""left"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;border-top: 1px solid #ccc;padding-top: 16px;padding-left: 0;padding-right: 0;padding-bottom: 16px;"">
                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                          <tr>
                            <td class=""u-bold"" align=""left"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;font-weight: bold;"">
                              Greco(le)
                            </td>
                            <td class=""u-bold"" align=""right"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;font-weight: bold;"">
                              €13.20
                            </td>
                          </tr>
                          <tr>
                            <td class=""u-greySmaller u-padding__top--half"" align=""left"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 13px !important;padding-top: 16px;padding-left: 0;padding-right: 0;padding-bottom: 0;color: #737373;"">
                              16 January 2020 12:10:07
                            </td>
                            <td class=""u-greySmaller u-padding__top--half"" align=""right"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 13px !important;padding-top: 16px;padding-left: 0;padding-right: 0;padding-bottom: 0;color: #737373;"">
                              
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td class=""u-padding__top u-border__top"" align=""left"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;border-top: 1px solid #ccc;padding-top: 32px;padding-left: 0;padding-right: 0;padding-bottom: 0;"">
                        On this card:
                      </td>
                    </tr>
                    <tr>
                      <td class=""u-padding__topBottom"" align=""left"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;padding-top: 32px;padding-left: 0;padding-right: 0;padding-bottom: 32px;"">
                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                          <tr>
                            <td align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                              <img src=""https://cardimages.imaginecurve.com/cards/546898.png"" class=""columnImage"" width=""200"" style=""max-width: 100%;border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                            </td>
                          </tr>
                          <tr>
                            <td class=""u-padding__top--half"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;padding-top: 16px;padding-left: 0;padding-right: 0;padding-bottom: 0;"">
                                                              Simon Baudart<br>
                                BlaBlaBank<br>
                                XXXX-9999<br>
                                                          </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td class=""u-greySmaller u-padding__topBottom u-border__top"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 13px !important;border-top: 1px solid #ccc;padding-top: 32px;padding-left: 0;padding-right: 0;padding-bottom: 32px;color: #737373;"">
                        Receipt for the purchase (add it using the Curve App):
                        <br>
                        <br>
                        <img src=""https://api.imaginecurve.com/receipts/eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJyaWQiOjQ0ODAyNTI3LCJpYXQiOjE1NzkxNzM4MzYsImp0aSI6IjQ4YzhmYjQzY2IzMjgyYWQ0MTgxZDI5MjVkYWZlNGNjIn0.zn5lppH34S82SK57GbOz_JDI4RiocPF_4V0xJkxgC1IXDSeC5xOuxVEQk7kC-B98NJXVdIS0xUE6jt5uJtt4OA"" width=""320"" style=""max-width: 80%;border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                      </td>
                    </tr>
                    <tr>
                      <td class=""u-greySmaller u-padding__top u-border__top"" align=""center"" valign=""top"" style=""padding-bottom: 0;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 13px !important;border-top: 1px solid #ccc;padding-top: 32px;padding-left: 0;padding-right: 0;color: #737373;"">
                                                  This Transaction will appear on your bank statement as: <br>
                          GRECO(LE)              TOURNAI       BEL
                                              </td>
                    </tr>
                  </tbody>
                </table>
              </td>
            </tr>
            <tr>
              <td class=""u-greySmaller u-padding__top--half"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 13px !important;padding-top: 16px;padding-left: 0;padding-right: 0;padding-bottom: 0;color: #737373;"">
                Generated on 16 January 2020 11:23 UTC
              </td>
            </tr>
            <tr>
              <td align=""center"" valign=""top"" id=""ShareContainer"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;padding-top: 96px;padding-left: 0;padding-right: 0;padding-bottom: 96px;background-image: url(https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/bank-notes.png);background-repeat: no-repeat;background-size: 160px;background-position: center right;"">
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                  <tr>
                    <td class=""ShareHeader"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;position: relative;"">
                      <h2 style=""display: block;margin: 0;padding: 0;font-size: 18px;"">Share Curve And Get  £5 </h2>
                      <br>
                      <span>Refer a friend, and you both get  £5 </span>
                    </td>
                  </tr>
                  <tr>
                    <td class=""u-padding__topBottom"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;padding-top: 32px;padding-left: 0;padding-right: 0;padding-bottom: 32px;"">
                      <table border=""0"" cellpadding=""10"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                        <tr>
                          <td width=""50%"" align=""right"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                            <a target=""_blank"" href=""https://www.facebook.com/sharer/sharer.php?app_id=1579201099059872&u=https%3A%2F%2Fwww.curve.app%2Fjoin%3Futm_source%3Demailreceipt%26utm_medium%3Demail%26utm_campaign%3Dfb_share%23HEP8T"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">
                              <img src=""https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/icon-facebook.png"" width=""50"" style=""border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                            </a>
                          </td>
                          <td width="""" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                            <a target=""_blank"" href=""https://twitter.com/intent/tweet?url=https%3A%2F%2Fwww.curve.app%2Fjoin%3Futm_source%3Demailreceipt%26utm_medium%3Demail%26utm_campaign%3Dtwitter%23HEP8T&text=Get+Curve+%26+Earn+%C2%A35"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">
                              <img src=""https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/icon-twitter.png"" width=""50"" style=""border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                            </a>
                          </td>
                          <td width="""" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                            <a target=""_blank"" href=""https://api.whatsapp.com/send?phone=&text=Get%20Curve%20%26%20Earn%20%C2%A35%20https%3A%2F%2Fwww.curve.app%2Fjoin%3Futm_source%3Demailreceipt%26utm_medium%3Demail%26utm_campaign%3Dwhatsapp%23HEP8T"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">
                              <img src=""https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/icon-whatsapp.png"" width=""50"" style=""border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                            </a>
                          </td>
                          <td width=""50%"" align=""left"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                            <a target=""_blank"" href=""https://www.facebook.com/dialog/send?app_id=1579201099059872&redirect_uri=https%3A%2F%2Fwww.curve.app%2Fjoin%3Futm_source%3Demailreceipt%26utm_medium%3Demail%26utm_campaign%3Dfb_msg%23HEP8T&link=https%3A%2F%2Fwww.curve.app%2Fjoin%3Futm_source%3Demailreceipt%26utm_medium%3Demail%26utm_campaign%3Dfb_msg%23HEP8T&display=popup"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">
                              <img src=""https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/icon-messenger.png"" width=""50"" style=""border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                            </a>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                      Or they can <a href=""https://www.curve.app/join?utm_source=emailreceipt&utm_medium=email&utm_campaign=direct#HEP8T"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">sign up to Curve</a> using your referral code: <span style=""font-family: monospace;""><a href=""https://www.curve.app/join?utm_source=emailreceipt&utm_medium=email&utm_campaign=direct#HEP8T"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">HEP8T</a></span>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td class=""u-padding__topBottom"" align=""center"" valign=""top"" id=""AppContainer"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;padding-top: 32px;padding-left: 0;padding-right: 0;padding-bottom: 32px;"">
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                  <tr>
                    <td align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                      <a href=""https://www.curve.app/?utm_source=emailreceipt&utm_medium=email&utm_campaign=direct"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">
                        <img src=""https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/curve-logo.png"" style=""max-width: 200px;border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                      </a>
                    </td>
                  </tr>
                  <tr>
                    <td class=""u-padding__topBottom--half"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;padding-top: 16px;padding-left: 0;padding-right: 0;padding-bottom: 16px;"">
                      <table border=""0"" cellpadding=""5"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse;mso-table-lspace: 0pt;mso-table-rspace: 0pt;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                        <tr>
                          <td width=""50%"" align=""right"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                            <a href=""https://imaginecurve.onelink.me/3972912079/c5b9d717"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">
                              <img src=""https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/icon-appstore.png"" width=""140"" style=""max-width: 100%;border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                            </a>
                          </td>
                          <td width=""50%"" align=""left"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;"">
                            <a href=""https://imaginecurve.onelink.me/3972912079/c5b9d717"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">
                              <img src=""https://res.cloudinary.com/dzatxn6bx/image/upload/v1561119616/email/icon-playstore.png"" width=""140"" style=""max-width: 100%;border: 0;height: auto;line-height: 100%;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;"">
                            </a>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td class=""u-padding__topBottom"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;padding-top: 32px;padding-left: 0;padding-right: 0;padding-bottom: 32px;"">
                <a href=""https://imaginecurve.zendesk.com/hc/en-gb/articles/214357205"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">How to stop receiving Email Receipts</a>
                <br>
                <br>
                <a href=""mailto:support@imaginecurve.com?subject=Receipt Help: Curve Receipt: Purchase at Greco(le) on 16 January 2020 for €13.20"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 15px;color: #000000;font-weight: normal;text-decoration: underline;"">Something not correct? Contact Curve Support!</a>
              </td>
            </tr>
            <tr>
              <td class=""u-greySmaller u-padding__top"" align=""center"" valign=""top"" style=""mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;font-size: 13px !important;padding-top: 32px;padding-left: 0;padding-right: 0;padding-bottom: 0;color: #737373;"">
                <em>MasterCard is a registered trademark of MasterCard International Incorporated. The card is issued by Wirecard Card Solutions Ltd (WDCS) pursuant to license by MasterCard International Inc. WDCS is authorised by the Financial Conduct Authority to conduct electronic money service activities under the Electronic Money Regulations 2011 (Ref: 900051)</em>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </center>
</body>

</html>";

        [Fact]
        public void ExecuteOkInTextBody()
        {
            var message = new Message();
            message.Add("textBody", EmailTextHtmlGreco);

            var job = this.CreateJob();
            var processor = new CurveReceipt()
            {
                ProcessorConfiguration = new Processor
                {
                    Id = Guid.NewGuid().ToString()
                }
            };
            processor.Execute(job, message);

            var transaction = message.Get<TransactionInformation>(TransactionInformation.MessagePropertyName);
            Assert.NotNull(transaction);
            Assert.Equal(-13.20M ,transaction.Amount);
            Assert.Equal("BlaBlaBank" ,transaction.CardName);
            Assert.Equal(string.Empty, transaction.Category);
            Assert.Equal("Greco(le)" ,transaction.Creditor);
            Assert.Equal("EUR" ,transaction.Currency);
            Assert.Equal(string.Empty ,transaction.Note);
            Assert.Equal(new DateTime(2020,01,16,12,10,07).ToUniversalTime(), transaction.UtcDate);
        }

        [Fact]
        public void ExecuteOkInHtmlBody()
        {
          var message = new Message();
          message.Add("htmlBody", EmailTextHtmlGreco);

          var job = this.CreateJob();
          var processor = new CurveReceipt()
          {
            ProcessorConfiguration = new Processor
            {
              Id = Guid.NewGuid().ToString()
            }
          };
          processor.Execute(job, message);

          var transaction = message.Get<TransactionInformation>(TransactionInformation.MessagePropertyName);
          Assert.NotNull(transaction);
          Assert.Equal(-13.20M ,transaction.Amount);
          Assert.Equal("BlaBlaBank" ,transaction.CardName);
          Assert.Equal(string.Empty, transaction.Category);
          Assert.Equal("Greco(le)" ,transaction.Creditor);
          Assert.Equal("EUR" ,transaction.Currency);
          Assert.Equal(string.Empty ,transaction.Note);
          Assert.Equal(new DateTime(2020,01,16,12,10,07).ToUniversalTime(), transaction.UtcDate);
        }

        private Job CreateJob()
        {
            return new Job
            {
                Id = Guid.NewGuid().ToString(),
            };
        }
    }
}