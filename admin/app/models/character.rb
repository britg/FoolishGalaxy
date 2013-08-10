class Character < ActiveRecord::Base
  attr_accessible :age, :description, :name, :role, :sex
end
