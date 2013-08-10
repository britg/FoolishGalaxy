class CreateCharacters < ActiveRecord::Migration
  def change
    create_table :characters do |t|
      t.string :name
      t.string :role, :default => "Ensign"
      t.string :description
      t.integer :age, :default => 36
      t.string :sex, :default => "M"
    end
  end
end
