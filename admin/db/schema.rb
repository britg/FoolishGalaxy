# encoding: UTF-8
# This file is auto-generated from the current state of the database. Instead
# of editing this file, please use the migrations feature of Active Record to
# incrementally modify your database, and then regenerate this schema definition.
#
# Note that this schema.rb definition is the authoritative source for your
# database schema. If you need to create the application database on another
# system, you should be using db:schema:load, not running all the migrations
# from scratch. The latter is a flawed and unsustainable approach (the more migrations
# you'll amass, the slower it'll run and the greater likelihood for issues).
#
# It's strongly recommended to check this file into your version control system.

ActiveRecord::Schema.define(:version => 20130715111014) do

  create_table "active_admin_comments", :force => true do |t|
    t.string   "resource_id",   :null => false
    t.string   "resource_type", :null => false
    t.integer  "author_id"
    t.string   "author_type"
    t.text     "body"
    t.datetime "created_at",    :null => false
    t.datetime "updated_at",    :null => false
    t.string   "namespace"
  end

  add_index "active_admin_comments", ["author_type", "author_id"], :name => "index_active_admin_comments_on_author_type_and_author_id"
  add_index "active_admin_comments", ["namespace"], :name => "index_active_admin_comments_on_namespace"
  add_index "active_admin_comments", ["resource_type", "resource_id"], :name => "index_admin_notes_on_resource_type_and_resource_id"

  create_table "admin_users", :force => true do |t|
    t.string   "email",                  :default => "", :null => false
    t.string   "encrypted_password",     :default => "", :null => false
    t.string   "reset_password_token"
    t.datetime "reset_password_sent_at"
    t.datetime "remember_created_at"
    t.integer  "sign_in_count",          :default => 0
    t.datetime "current_sign_in_at"
    t.datetime "last_sign_in_at"
    t.string   "current_sign_in_ip"
    t.string   "last_sign_in_ip"
    t.datetime "created_at",                             :null => false
    t.datetime "updated_at",                             :null => false
  end

  add_index "admin_users", ["email"], :name => "index_admin_users_on_email", :unique => true
  add_index "admin_users", ["reset_password_token"], :name => "index_admin_users_on_reset_password_token", :unique => true

  create_table "characters", :force => true do |t|
    t.string  "name"
    t.string  "role",        :default => "Ensign"
    t.string  "description"
    t.integer "age",         :default => 36
    t.string  "sex",         :default => "M"
  end

  create_table "destinations", :force => true do |t|
    t.string "classification"
    t.float  "x",              :default => 0.0
    t.float  "y",              :default => 0.0
    t.string "name",           :default => "Unknown Object"
    t.string "description",    :default => "Scanners detect a faint signal but its signature does not match anything in our database."
    t.text   "data",           :default => "{}"
  end

  add_index "destinations", ["x", "y"], :name => "index_destinations_on_x_and_y"

  create_table "event_templates", :force => true do |t|
    t.integer "character_id"
    t.string  "message",          :default => ""
    t.text    "data",             :default => "{}"
    t.string  "context",          :default => "story"
    t.string  "arc",              :default => "main"
    t.integer "branch",           :default => 0
    t.integer "sequence",         :default => 0
    t.integer "next_event_delay", :default => 0
    t.boolean "pause",            :default => false
  end

  add_index "event_templates", ["arc"], :name => "index_event_templates_on_arc"
  add_index "event_templates", ["context"], :name => "index_event_templates_on_context"

  create_table "events", :force => true do |t|
    t.integer "character_id"
    t.string  "message",       :default => ""
    t.text    "data",          :default => "{}"
    t.string  "context",       :default => "story"
    t.string  "arc",           :default => "main"
    t.integer "sequence",      :default => 0
    t.integer "branch",        :default => 0
    t.string  "current_state", :default => "new"
    t.integer "timestamp"
    t.boolean "pause",         :default => false
  end

  add_index "events", ["arc"], :name => "index_events_on_arc"
  add_index "events", ["context"], :name => "index_events_on_context"

  create_table "players", :force => true do |t|
    t.string  "name"
    t.integer "minerals",       :default => 0
    t.integer "gas",            :default => 0
    t.integer "biomass",        :default => 0
    t.float   "x",              :default => 0.0
    t.float   "y",              :default => 0.0
    t.float   "destination_x",  :default => 0.0
    t.float   "destination_y",  :default => 0.0
    t.float   "scanner_radius", :default => 1000.0
  end

end
