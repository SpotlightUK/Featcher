require 'git'
require 'optparse'

options = {}

optparse = OptionParser.new do |opts|
	opts.banner = 'Usage: mergecount.rb [options]'
	opts.on("-j", "--major VERSION", "Major version number e.g. 0, 1") do |j|
		options[:major_version] = j
	end
	opts.on("-n", "--minor MINOR", "Minor version number, e.g. 1, 2") do |n|
		options[:minor_version] = n
	end
	opts.on("-h", "--help", "Display this screen") do
		puts opts
		exit
	end

end.parse!

mandatory = [:major_version, :minor_version]
missing = mandatory.select{ |param| options[param].nil? }
if not missing.empty?
	puts "Missing required arguments: #{missing.join(', ')}"
	puts optparse
	exit
end

major_version = options[:major_version] # 0
minor_version = options[:minor_version] # 0

path = File.join((File.expand_path File.dirname(__FILE__)), "../..")
puts path

repo = Git.open(path)
print "Counting version tags from v#{major_version}.#{minor_version}.0 (inclusive)...\n"
regexp = Regexp.new("v#{major_version}\\.#{minor_version}\\.(\\d+)")
version_tags = repo.tags.select { |tag| regexp.match(tag.name) }
tags_count = version_tags.length
print "Found #{tags_count} version tags. Checking for gaps between the patch numbers...\n"

t = version_tags.each do 
	|tag| tag.name.scan(regexp) do 
		|m| patch = m[0].to_i 
		if patch >= tags_count
			print "Found a gap! Patch number: #{patch} \n\n"
			tags_count = patch + 1
		end
	end
end

puts "The current patch number is #{tags_count}"
puts "##teamcity[setParameter name='PatchVersion' value='#{tags_count}']"

