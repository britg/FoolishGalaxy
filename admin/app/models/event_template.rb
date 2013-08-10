class EventTemplate < ActiveRecord::Base
  attr_accessible *column_names
  belongs_to :character
end
