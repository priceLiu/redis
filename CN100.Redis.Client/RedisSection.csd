<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="7a3944b3-4166-4e51-b0ce-66d8b84e3e1a" namespace="CN100.Redis.Client" xmlSchemaNamespace="urn:CN100.Redis.Client" assemblyName="CN100.Redis.Client" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
    <enumeratedType name="ReadMod" namespace="System">
      <literals>
        <enumerationLiteral name="ReadAndWrite" />
        <enumerationLiteral name="ReadOnly" />
      </literals>
    </enumeratedType>
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="RedisSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="RedisSection">
      <elementProperties>
        <elementProperty name="RedisGroup" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="RedisGroup" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/RedisGroup" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="host" xmlItemName="add" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="db" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="db" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/Int64" />
          </type>
        </attributeProperty>
        <attributeProperty name="connectName" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="connectName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MaxReadPoolSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="maxReadPoolSize" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="MaxWritePoolSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="maxWritePoolSize" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/Server" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Server">
      <attributeProperties>
        <attributeProperty name="ipAddress" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="ipAddress" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="RedMod" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="redMod" isReadOnly="false">
          <type>
            <enumeratedTypeMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/ReadMod" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="RedisGroup" xmlItemName="host" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementCollectionMoniker name="/7a3944b3-4166-4e51-b0ce-66d8b84e3e1a/host" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>