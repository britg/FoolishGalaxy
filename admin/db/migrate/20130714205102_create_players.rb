class CreatePlayers < ActiveRecord::Migration
  def change
    create_table :players do |t|
      t.string :name
      t.integer :minerals, :default => 0
      t.integer :gas, :default => 0
      t.integer :biomass, :default => 0
      t.float :x, :default => 0
      t.float :y, :default => 0
      t.float :destination_x, :default => 0
      t.float :destination_y, :default => 0
      t.float :scanner_radius, :default => 1000
    end
  end
end
