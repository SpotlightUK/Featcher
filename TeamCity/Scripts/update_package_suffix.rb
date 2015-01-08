require 'optparse'

options = {}

optparse = OptionParser.new do |opts|
    opts.banner = 'Usage: ruby update_package_suffix.rb [options]'
    opts.on("-b", '--teamcity-build-branch BRANCH', "Pass in teamcity.build.branch") do |b| 
        options[:teamcity_build_branch] = b
    end
    opts.on("-h", "--help", "Display this screen") do
        puts opts
        exit
    end

end.parse!

puts "update_package_suffix.rb -b #{options[:teamcity_build_branch]}"

is_master = (options[:teamcity_build_branch] == "refs/heads/master")

if is_master
    # Empty package suffix for master builds
    puts 'Setting package suffix for master build'
    puts "##teamcity[setParameter name='PackageSuffix' value='']"
else
    puts 'Setting package suffix for feature branch'
    # builds from pull requests are tagged with the branch number
    puts "##teamcity[setParameter name='PackageSuffix' value='-branch#{options[:teamcity_build_branch]}']"
end