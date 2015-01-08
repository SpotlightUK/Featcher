require "rexml/text"
require 'rexml/document'
include REXML
require 'optparse'

options = {}
# ruby update_apsetting.rb -f ../../Sources/Spotlight.Casting.AppService/App.config --k CurrentAppVersion --value 1.2.3.4 

optparse = OptionParser.new do |opts|
    opts.banner = 'Usage: ruby update_package_suffix.rb [options]'
    opts.on("-f", "--file PATH", "The XML file to be updated") do |f| 
        options[:file_path] = f
    end
    opts.on("-k", "--key KEY", "Key of the config setting to be updated") do |k|
        options[:key] = k
    end
    opts.on("-v", "--value VALUE", "New value of the attribute") do |v|
    	options[:value] = v
    end
    opts.on("-h", "--help", "Display this screen") do
        puts opts
        exit
    end
end.parse!

xpath = "/configuration/appSettings/add[@key='#{options[:key]}']"

puts "Updating value in #{options[:file_path]}..."
puts "Using XPath #{xpath}"
config = File.open(options[:file_path]).read

xml = Document.new(config)
puts "Writing new value #{options[:value]}"
xml.root.elements[xpath].attributes['value'] = options[:value]

File.open(options[:file_path],"w") do |data|
	data<<xml
end