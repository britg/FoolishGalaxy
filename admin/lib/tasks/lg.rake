namespace :lg do
  task :reset_db => :environment do
    system 'rm -f /Users/Eezo/Library/Caches/brit_britg_com/Lonely\ Galaxy/lg.db'
    system 'rm -f /Users/Eezo/Projects/Lonely\ Galaxy/Assets/StreamingAssets/lg.db'
    system 'cp /Users/Eezo/Projects/Lonely\ Galaxy/admin/db/lg.sqlite3 /Users/Eezo/Projects/Lonely\ Galaxy/Assets/StreamingAssets/lg.db'
  end
end