class Event < ActiveRecord::Base
  attr_accessible *column_names

  class << self

    def seed_dummy_events count = 100
      count.times do |i|
        Event.create :message => "This is an event #{i} #{random_string}"
      end
    end

    def ipsum
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed luctus gravida magna vitae luctus. Donec eget neque ut erat sagittis placerat eget non magna. Sed sed ligula sem. Integer tincidunt et mi in ullamcorper. Mauris auctor felis sed neque imperdiet, ut accumsan lorem lobortis. Duis sollicitudin a quam ac malesuada. Fusce imperdiet placerat tincidunt."
    end

    def random_string
      start = (Random.rand*10).round
      finish = (Random.rand*ipsum.length).round
      ipsum.split(" ")[start..finish].join(' ')
    end

  end
end
