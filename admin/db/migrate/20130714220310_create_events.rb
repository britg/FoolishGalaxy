class CreateEvents < ActiveRecord::Migration
  def change
    create_table :events do |t|
      t.references :character
      t.string :message, :default => ""
      t.text :data, :default => "{}"
      t.string :context, :default => "story"
      t.string :arc, :default => "main"
      t.integer :sequence, :default => 0
      t.integer :branch, :default => 0
      t.string :current_state, :default => "new"
      t.integer :timestamp
      t.boolean :pause, :default => false
    end

    add_index :events, :context
    add_index :events, :arc
  end
end
