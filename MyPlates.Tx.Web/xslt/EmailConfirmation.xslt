<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="text" indent="yes"/>

  <xsl:template match="/">
    <xsl:text><![CDATA[<span style="font-size: 32px; font: Arial, Helvetica, sans-serif; color: #333;">]]>Congratulations, your order was successful!<![CDATA[</span><br /><br />]]></xsl:text>
    <xsl:text><![CDATA[<span style="font-weight: bold; font-size: 28px; font: Arial, Helvetica, sans-serif; color: #333;">]]>Your Order ID is: </xsl:text><xsl:value-of select='/NewDataSet/Table/Order_ID'/><![CDATA[</span><br /><br />]]>

    <![CDATA[<span style="font-size: 16px; font: Arial, Helvetica, sans-serif; color: #333;">]]>
    <xsl:text>Please allow </xsl:text><![CDATA[<span style="font-weight: bold;">]]>3-4 weeks<![CDATA[</span>]]><xsl:text> for your plates to be manufactured and shipped to the county of your choice.</xsl:text><![CDATA[<br /><br />]]>

    <xsl:text>We also encourage you to call your local county office to confirm that your plates have been delivered for you to collect them.</xsl:text><![CDATA[<br /><br />]]>

    <xsl:text>All personalized plate selections are subject to additional review and approval by TxDOT. If denied, one of our customer service representatives will contact you and provide you the opportunity to make another selection.</xsl:text><![CDATA[<br /><br />]]>

    <xsl:text><![CDATA[<span style="font-weight: bold; font-size: 24px; font: Arial, Helvetica, sans-serif; color: #333;">]]>Order Summary<![CDATA[</span>]]></xsl:text><![CDATA[<br /><br />]]>

    <xsl:for-each select="NewDataSet/Table">
      <![CDATA[<img]]><xsl:text> src='</xsl:text><xsl:value-of select='PlateGuid'/><xsl:text>' width='417' height='209' alt='Plate Combination is: </xsl:text><xsl:value-of select='ReceiptText'/>'<![CDATA[ />]]><![CDATA[<br />]]>
      <![CDATA[<strong>]]><xsl:value-of select='CategoryName'/><![CDATA[</strong><br />]]>
      <xsl:value-of select='TypeName'/><xsl:text> (</xsl:text><xsl:value-of select='RenewalPeriod'/><xsl:text> year term for </xsl:text><![CDATA[<strong>]]>$<xsl:value-of select='format-number(TotalPaid, "#.00")'/><![CDATA[</strong>]]><xsl:text>)</xsl:text><![CDATA[<br />]]>
      <xsl:value-of select='OwnerFirstName'/><xsl:text> </xsl:text><xsl:value-of select='OwnerLastName'/><xsl:text> (Owner)</xsl:text><![CDATA[<br /><br />]]>
      <xsl:text>Will be delivered to </xsl:text><![CDATA[<span style="font-weight: bold;">]]><![CDATA[</span><br />]]>
      <xsl:value-of select='OwnerCounty'/><xsl:text> County</xsl:text><![CDATA[</span><br />]]>
      <xsl:value-of select='CountyAddress'/>
      <![CDATA[<br /><br />]]>
    </xsl:for-each>
    
    <xsl:text><![CDATA[<span style="font-weight: bold; font-size: 20px; font: Arial, Helvetica, sans-serif; color: #333;">]]>Order Total: $</xsl:text><xsl:value-of select='format-number(sum(NewDataSet/Table/TotalPaid), "#.00")'/><![CDATA[</span><br /><br />]]>

    <xsl:text>Specialty plate fees will not be refunded once application is submitted.</xsl:text><![CDATA[<br /><br />]]>

    <xsl:text>Actual plate shown is for illustrative purposes only. The color as presented on your screen may vary to what is actually represented on the manufactured plate.</xsl:text><![CDATA[<br /><br /><br />]]>

    <xsl:text><![CDATA[<span style="font-weight: bold; font-size: 24px; font: Arial, Helvetica, sans-serif; color: #333;">]]>Billing Information<![CDATA[</span>]]></xsl:text><![CDATA[<br /><br />]]>

    <xsl:value-of select='NewDataSet/Table/CustomerFirstName'/><xsl:text> </xsl:text><xsl:value-of select='NewDataSet/Table/CustomerLastName'/><![CDATA[<br />]]>
    <xsl:value-of select='NewDataSet/Table/CustomerStreet1'/><![CDATA[<br />]]>
    <xsl:if test='NewDataSet/Table/CustomerStreet2!=""'>
      <xsl:value-of select='NewDataSet/Table/CustomerStreet2'/><![CDATA[<br />]]>
    </xsl:if>
    <xsl:value-of select='NewDataSet/Table/CustomerCity'/><xsl:text>, </xsl:text><xsl:value-of select='NewDataSet/Table/CustomerState'/><xsl:text> </xsl:text><xsl:value-of select='NewDataSet/Table/CustomerZIP'/><![CDATA[<br />]]>

    <![CDATA[<br /><br /><br /><img]]><xsl:text> src='http://tst.myplates.com/img/myplates.jpg'</xsl:text><![CDATA[ /><br />]]>
    <xsl:text>1-877-MY-PLATES (1-877-769-7528)</xsl:text>
    <![CDATA[</span>]]>
  </xsl:template>
</xsl:stylesheet>