class CreateEventTemplates < ActiveRecord::Migration
  def change
    create_table :event_templates do |t|
      t.references :character
      t.string :message, :default => ""
      t.text :data, :default => "{}"
      t.string :context, :default => "story"
      t.string :arc, :default => "main"
      t.integer :branch, :default => 0
      t.integer :sequence, :default => 0
      t.integer :next_event_delay, :default => 0
      t.boolean :pause, :default => false
    end

    add_index :event_templates, :context
    add_index :event_templates, :arc
  end
end
