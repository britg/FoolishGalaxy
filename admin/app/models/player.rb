class Player < ActiveRecord::Base
  attr_accessible *column_names

  class << self
    def seed
      Player.create(:minerals => 50,
                    :gas => 50,
                    :biomass => 50)
    end
  end
end
