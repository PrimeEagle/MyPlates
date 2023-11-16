<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="text" indent="yes"/>

  <xsl:template match="/">
	  <![CDATA[<PRODUCTS>]]>
    <![CDATA[<CATEGORIES>]]>
      
      <xsl:for-each select="NewDataSet/TableCat">
		  <![CDATA[<CATEGORY]]>
              ID="<xsl:value-of select='Category_ID'/>"
              NAME="<xsl:value-of select='CatName'/>"
              YR1="<xsl:value-of select='YR1'/>"
              YR5="<xsl:value-of select='YR5'/>"
              YR10="<xsl:value-of select='YR10'/>"
        <![CDATA[>]]>

		<![CDATA[<SHORTDESCRIPTION>]]>
        <xsl:value-of select='ShortDesc'/>
        <![CDATA[</SHORTDESCRIPTION>]]>

        <![CDATA[<LONGDESCRIPTION>]]>
        <xsl:value-of select='LongDesc'/>
        <![CDATA[</LONGDESCRIPTION>]]>

        <![CDATA[<PLATES>]]>
        <xsl:value-of select='Plates'/>
        <![CDATA[</PLATES>]]>
        
        <![CDATA[</CATEGORY>]]>
      </xsl:for-each>
      
      <![CDATA[</CATEGORIES>]]>


    <![CDATA[<SECTIONS>]]>

    <xsl:for-each select="NewDataSet/TableSection">
      <![CDATA[<SECTION]]>
      ID="<xsl:value-of select='Section_ID'/>"
      NAME="<xsl:value-of select='Name'/>"
      <![CDATA[>]]>

      <![CDATA[</SECTION>]]>
    </xsl:for-each>

    <![CDATA[</SECTIONS>]]>    
    
      <![CDATA[<PLATES>]]>
      <xsl:for-each select="NewDataSet/TablePlate">
      <![CDATA[<PLATE]]>
      CODE="<xsl:value-of select='PlateCode_ID'/>"
      TYPE="<xsl:value-of select='PlateName'/>"
      SUBCATEGORY="<xsl:value-of select='PlateTypeName'/>"
      XPOS="<xsl:value-of select='XPos'/>"
      YPOS="<xsl:value-of select='YPos'/>"
      ALIGN="<xsl:value-of select='Align'/>"
      COLOR1="<xsl:value-of select='Color1'/>"
      COLOR2="<xsl:value-of select='Color2'/>"
      CATEGORY_ID="<xsl:value-of select='Category_ID'/>"
      CATEGORY_NAME="<xsl:value-of select='CatName'/>"
      SECTION_NAME="<xsl:value-of select='SectionName'/>"
      VEHICLETYPE="<xsl:value-of select='VehicleType'/>"
      REGEX="<xsl:value-of select='RegEx'/>"
      MIN_SPACES="<xsl:value-of select='MinSpaces'/>"
      MAX_SPACES="<xsl:value-of select='MaxSpaces'/>"
      OPTIONAL_SPACES="<xsl:value-of select='OptionalSpaces'/>"
      MIN_CHARACTERS="<xsl:value-of select='MinCharacters'/>"
      MAX_CHARACTERS="<xsl:value-of select='MaxCharacters'/>"
      DIVIDED="<xsl:value-of select='Divided'/>"
      CLASS="<xsl:value-of select='Class'/>"
      <![CDATA[/>]]>

      </xsl:for-each>
      <![CDATA[</PLATES>]]>
    
    <![CDATA[</PRODUCTS>]]>
    </xsl:template>
  </xsl:stylesheet>
