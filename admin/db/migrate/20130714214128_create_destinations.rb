class CreateDestinations < ActiveRecord::Migration
  def change
    create_table :destinations do |t|
      t.string :classification
      t.float :x, :default => 0
      t.float :y, :default => 0
      t.string :name, :default => "Unknown Object"
      t.string :description, :default => "Scanners detect a faint signal but its signature does not match anything in our database."
      t.text :data, :default => "{}"
    end

    add_index :destinations, [:x, :y]
  end
end
