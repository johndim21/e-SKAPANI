//
//  GameBoardRenderer.swift
//  Escapani
//
//  Created by Panos Peltekis on 6/9/22.
//

import UIKit

final class GameBoardRenderer {
    
    private weak var boardView: GameBoardView?
    private var tileViews = [TileView]()
    private var size: CGFloat
    private var image: UIImage
    
    init(boardView: GameBoardView, size: Int, image: UIImage) {
        boardView.subviews.forEach { $0.removeFromSuperview() }
        self.boardView = boardView
        self.size = CGFloat(size)
        self.image = image
    }
    
    func setTiles(tiles: [Tile]) {
        tileViews.forEach { $0.removeFromSuperview() }
        tileViews.removeAll(keepingCapacity: true)
        tiles.forEach { addTile(tile: $0) }
    }
    
    private func addTile(tile: Tile) {
        let tileView = TileView(position: tile.position,
                                color: colorForTile(tile: tile))
        tileView.center = centerForTile(position: tile.position)
        tileView.alpha = 0.0
        boardView!.addSubview(tileView)
    
        var value = ""
        if let tileValue = tile.value as Int?, tileValue != 0 {
            value = "\(tileValue)"
            
            switch size {
            case 3:
                var pieceSize = image.size.width / 3
                switch tileValue {
                case 1:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 0, y: 0, width: pieceSize, height: pieceSize))
                case 2:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: pieceSize, y: 0, width: pieceSize, height: pieceSize))
                case 3:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 2 * pieceSize, y: 0, width: pieceSize, height: pieceSize))
                case 4:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 0, y: pieceSize, width: pieceSize, height: pieceSize))
                case 5:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: pieceSize, y: pieceSize, width: pieceSize, height: pieceSize))
                case 6:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 2 * pieceSize, y: pieceSize, width: pieceSize, height: pieceSize))
                case 7:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 0, y: 2 * pieceSize, width: pieceSize, height: pieceSize))
                case 8:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: pieceSize, y: 2 * pieceSize, width: pieceSize, height: pieceSize))
                default:
                    break
                }
            case 4:
                var pieceSize = image.size.width / 4
                switch tileValue {
                case 1:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 0, y: 0, width: pieceSize, height: pieceSize))
                case 2:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: pieceSize, y: 0, width: pieceSize, height: pieceSize))
                case 3:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 2 * pieceSize, y: 0, width: pieceSize, height: pieceSize))
                case 4:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 3 * pieceSize, y: 0, width: pieceSize, height: pieceSize))
                case 5:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 0, y: pieceSize, width: pieceSize, height: pieceSize))
                case 6:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: pieceSize, y: pieceSize, width: pieceSize, height: pieceSize))
                case 7:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 2 * pieceSize, y: pieceSize, width: pieceSize, height: pieceSize))
                case 8:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 3 * pieceSize, y: pieceSize, width: pieceSize, height: pieceSize))
                case 9:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 0, y: 2 * pieceSize, width: pieceSize, height: pieceSize))
                case 10:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: pieceSize, y: 2 * pieceSize, width: pieceSize, height: pieceSize))
                case 11:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 2 * pieceSize, y: 2 * pieceSize, width: pieceSize, height: pieceSize))
                case 12:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 3 * pieceSize, y: 2 * pieceSize, width: pieceSize, height: pieceSize))
                case 13:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 0, y: 3 * pieceSize, width: pieceSize, height: pieceSize))
                case 14:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: pieceSize, y: 3 * pieceSize, width: pieceSize, height: pieceSize))
                case 15:
                    tileView.imageView.image = image.cropImage(toRect: CGRect(x: 2 * pieceSize, y: 3 * pieceSize, width: pieceSize, height: pieceSize))
                default:
                    break
                }
            default:
                break
            }
        }
        
        var bounds = tileView.bounds
        bounds.size = tileSize
        tileView.bounds = bounds
        tileView.valueLabel.text = value
        tileView.valueLabel.frame = bounds
        tileView.imageView.frame = bounds
        tileView.valueLabel.font = UIFont.boldSystemFont(ofSize: tileSize.width/2)
        UIView.animate(withDuration: 0.2, animations: {
            tileView.alpha = 1.0
        }) { _ in }
        
        tileViews.append(tileView)
    }
    
    func swapTiles(isSettingUp: Bool, firstTile: Tile, secondTile: Tile, speed: TimeInterval, completionBlock: @escaping () -> Void) {
        if let firstTileView = tileViews.filter({$0.position == firstTile.position}).first as TileView?,
            let secondTileView = tileViews.filter({$0.position == secondTile.position}).first as TileView? {
            if isSettingUp {
                firstTileView.center = self.centerForTile(position: secondTile.position)
                secondTileView.center = self.centerForTile(position: firstTile.position)
                firstTileView.position = secondTile.position
                secondTileView.position = firstTile.position
                completionBlock()
            } else {
                UIView.animate(withDuration: speed, animations: {
                    firstTileView.center = self.centerForTile(position: secondTile.position)
                    secondTileView.center = self.centerForTile(position: firstTile.position)
                }) { _ in
                    firstTileView.position = secondTile.position
                    secondTileView.position = firstTile.position
                    completionBlock()
                }
            }
        }
    }
    
    func presentWinningState() {
        for tileView in tileViews {
            if let currentColor = tileView.backgroundColor as UIColor? {
                UIView.animate(withDuration: 0.2, animations: {
                    tileView.backgroundColor = currentColor.darker()
                })
            }
            tileView.isUserInteractionEnabled = false
        }
    }
    
    private func colorForTile(tile: Tile) -> UIColor {
        if tile.value == 0 {
            return .clear
        }
        switch size {
        case 3:
            return UIColor(rgb: 0xF17D80)
        case 4:
            return UIColor(rgb: 0x956D89)
        case 5:
            return UIColor(rgb: 0x385E92)
        case 6:
            return UIColor(rgb: 0x66A5AA)
        default:
            return UIColor(rgb: 0x354458)
        }
    }
    
    private func centerForTile(position: Position) -> CGPoint {
        let x = (offset * CGFloat(position.x)) + (tileSize.width * CGFloat(position.x)) + (tileSize.width / 2.0) + offset
        let y = (offset * CGFloat(position.y)) + (tileSize.height * CGFloat(position.y)) + (tileSize.height / 2.0) + offset
        return CGPoint(x: x, y: y)
    }
    
    private var offset: CGFloat {
        return 1.5
    }
    
    private var tileSize: CGSize {
        let edge = (defaultBoardSize - ((size+CGFloat(1))*offset))/size
        return CGSize(width: edge, height: edge)
    }
}
